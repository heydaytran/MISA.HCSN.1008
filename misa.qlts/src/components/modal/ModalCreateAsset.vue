<template>
  <div>
    <div v-show="isActive" class="modal">
      <div class="modal-background"></div>
      <div class="modal-content">
        <div class="header">
          <div v-if="formMode == 'insert'" class="title">Thêm mới tài sản</div>
          <div v-else class="title">Sửa tài sản</div>

          <div class="header-right">
            <div class="icon-help btn btn-help" title="Hỗ trợ"></div>
            <div
              class="icon-cancel btn btn-close"
              title="Đóng"
              @click="hide()"
            ></div>
          </div>
        </div>

        <div class="content">
          <div class="input-field">
            <label for="">Mã tài sản <span style="color:red">(*)</span></label>
            <input
              onClick="this.select();"
              id="assetInput1"
              type="text"
              maxlength="20"
              class="input-one-third"
              v-model="asset.assetCode"
              @keyup="validateAssetCode()"
              @blur="validateAssetCode()"
            />

            <div id="assetInput1_warning" class="validate-warning">
              Thông tin bắt buộc nhập
            </div>
            <div
              v-show="dup"
              style="display: block"
              id="assetInput1_warning"
              class="validate-warning"
            >
              Mã tài sản bị trùng
            </div>
          </div>
          <div class="input-field">
            <label for="">Tên tài sản <span style="color:red">(*)</span></label>
            <input
              id="assetInput2"
              maxlength="255"
              class="input-two-third"
              type="text"
              @keyup="validateAssetName()"
              @blur="validateAssetName()"
              v-model="asset.assetName"
            />

            <div id="assetInput2_warning" class="validate-warning">
              Thông tin bắt buộc nhập
            </div>
          </div>
          <div class="clear-float"></div>
          <div class="input-field">
            <label>Mã phòng ban</label>
            <v-autocomplete
              tag="div"
              id="assetInput3"
              class="custom-autocomplete input-one-third"
              :items="listDepartment"
              item-value="departmentId"
              item-text="departmentCode"
              v-model="asset.departmentId"
            ></v-autocomplete>
          </div>
          <div class="input-field">
            <label for="">Tên phòng ban</label>
            <input
              class="input-black input-two-third"
              tabindex="-1"
              type="text"
              name=""
              v-model="asset.departmentName"
            />
          </div>
          <div class="clear-float"></div>
          <div class="input-field">
            <label for="">Mã loại tài sản</label>
            <v-autocomplete
              id="assetInput4"
              class="custom-autocomplete input-one-third"
              :items="listAssetType"
              item-value="assetTypeId"
              item-text="assetTypeCode"
              v-model="asset.assetTypeId"
            ></v-autocomplete>
          </div>
          <div class="input-field">
            <label for="">Tên loại tài sản</label>
            <input
              class="input-black input-two-third"
              tabindex="-1"
              type="text"
              name=""
              v-model="asset.assetTypeName"
            />
          </div>
          <div class="clear-float"></div>
          <div class="input-field">
            <label for="">Thời gian sử dụng (năm)</label>
            <input
              id="assetInput5"
              class="input-one-third"
              maxlength="4"
              type="text"
              name=""
              @keypress="formatNumber($event)"
              v-model="asset.timeUse"
            />
          </div>
          <div class="input-field">
            <label for="">Tỷ lệ hao mòn (%)</label>
            <input
              id="assetInput6"
              class="input-one-third"
              maxlength="2"
              type="text"
              name=""
              @keypress="formatNumber($event)"
              v-model="asset.wearRate"
            />
          </div>
          <div class="input-field">
            <label for="">Nguyên giá</label>
            <input
              id="assetInput7"
              maxlength="13"
              class="input-one-third"
              type="text"
              name=""
              style="text-align: right"
              :value="formatedMoney"
              @keypress="formatNumber($event)"
              @keyup="updateInput('', $event)"
            />
          </div>
          <div class="clear-float"></div>
          <div class="input-field">
            <label for="">Giá trị hao mòn năm</label>
            <input
              style="text-align: right"
              id="assetInput8"
              maxlength="10"
              class="input-one-third"
              type="text"
              name=""
              :value="formatedWearValue"
              @keypress="formatNumber($event)"
              @keyup="updateInput('wearValue', $event)"
            />
          </div>
          <!-- <DatePicker
            class="input-sm"
            input-class="datetime" 
            placeholder="__/__/____"
            v-model="asset.createdDate" 
            type="date"
            format="DD/MM/YYYY"
           
            value-type="YYYY-MM-DD"
            
            :lang="lang"
            :disabled-date="disabledAfterToday"
            @input-error="showWarning('Ngày sai định dạng', true)"
          >
            <template v-slot:header="{ emit }">
              <button class="mx-btn mx-btn-text" @click="emit(new Date())">
                Today
              </button>
            </template>
          </DatePicker> -->
        </div>
        <div class="footer">
          <div class="btn btn-cancel" tabindex="0" @click="hide()">Hủy</div>

          <div class="btn btn-save" tabindex="0" @click="save()">Lưu</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script src="https://unpkg.com/vuejs-datepicker/dist/locale/translations/fr.js"></script>

<script>
import axios from "axios";
import DatePicker from "vue2-datepicker";
//  import 'vue2-datepicker/locale/vi';

export default {
  components: {
    DatePicker,
  },
  props: {
    listAssetType: Array,
    listDepartment: Array,
    formMode: String,
    assetIdUpdate: String,
  },
  data() {
    return {
      isActive: false,
      showSuccess: true,
      asset: {
        assetId: null,
        assetCode: null,
        assetName: null,
        assetTypeId: null,
        departmentId: null,
        timeUse: null,
        wearRate: null,
        originalPrice: "",
        wearValue: null,
        isUsed: false,
        departmentName: null,
        assetTypeName: null,
        createdBy: null,
        modifiedBy: null,
        createDate: "",
      },
      dup: false,
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        days: ["CN", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7"],
        months: [
          "Tháng 1",
          "Tháng 2",
          "Tháng 3",
          "Tháng 4",
          "Tháng 5",
          "Tháng 6",
          "Tháng 7",
          "Tháng 8",
          "Tháng 9",
          "Tháng 10",
          "Tháng 11",
          "Tháng 12",
        ],
        weekdays: [
          "Chủ nhật",
          "thứ hai",
          "thứ ba",
          "thứ tư",
          "thứ năm",
          "thứ sáu",
          "Thứ Bảy",
        ],
        weekdaysMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
        yearFormat: "YYYY",
        monthsShort: [
          "T01",
          "T02",
          "T03",
          "T04",
          "T05",
          "T06",
          "T07",
          "T08",
          "T09",
          "T10",
          "T11",
          "T12",
        ],
        monthFormat: "TMM",
      },
    };
  },
  methods: {
    // todo reset lại các input
    resetInput() {
      (this.asset.assetId = null),
        (this.asset.assetCode = null),
        (this.asset.assetName = null),
        (this.asset.assetTypeId = null),
        (this.asset.departmentId = null),
        (this.asset.timeUse = null),
        (this.asset.wearRate = null),
        (this.asset.originalPrice = null),
        (this.asset.wearValue = null),
        (this.asset.isUsed = false),
        (this.asset.departmentName = null),
        (this.asset.assetTypeName = null),
        (this.asset.createdBy = null),
        (this.asset.modifiedBy = null);
    },
    // todo hiện dialog
    async show() {
      var res = this;
      this.isActive = true;
      this.dup = false

      var warning = document.getElementById("assetInput1");

      warning.classList.remove("borderRed");
      warning.classList.remove("hover-validate");

       var warning1 = document.getElementById("assetInput2");

      warning1.classList.remove("borderRed");
      warning1.classList.remove("hover-validate");

      // document.getElementById("assetInput1_warning").style.display = "none"

      // focus vào input đầu tiên( mã tài sản)

      setTimeout(() => {
        document.getElementById("assetInput1").focus();
      }, 0);
      //  document.getElementById("assetInput1").focus();

      document.getElementsByClassName("body-right")[0].style.zIndex = "999";
      if (this.formMode == "update") {
        await axios
          .get("https://localhost:44382/api/v1/Assets/" + this.assetIdUpdate)
          .then((Response) => {
            res.asset.departmentId = "";
            res.asset = Response.data.data;

            // debugger; // eslint-disable-line no-debugger
          })
          .catch((error) => {
            this.errorMessage = error.message;
            console.error("GET Asset by id Failed: ", error.message);
          });

        // convert ngyên giá sang string
        var a = res.asset.originalPrice;
        if (a == null) a = "";
        else a = res.asset.originalPrice.toString();

        res.asset.originalPrice = a;
        //debugger; // eslint-disable-line no-debugger
      } else {
        this.resetInput();
      }
    },

    // todo ẩn dialog
    hide() {
      this.isActive = false;
      document.getElementsByClassName("body-right")[0].style.zIndex = "0";
    },
    
// todo select tất cả nội dung ô input khi click
      selectAll()
    {
      document.getElementsByTagName("input").forEach(element => {
        element.addEventListener("click", ()=>{
          element.select()
        })
      });
    },

    // todo lấy dữ liệu tên phòng ban
    getDepartmentName() {
      var res = this;
      this.listDepartment.forEach((element) => {
        if (element.departmentId == res.asset.departmentId) {
          res.asset.departmentName = element.departmentName;
        }
      });
    },

    //todo lấy dữ liệu tên loại tài sản
    getAssetTypeName() {
      var res = this;
      this.listAssetType.forEach((element) => {
        if (element.assetTypeId == res.asset.assetTypeId) {
          res.asset.assetTypeName = element.assetTypeName;
        }
      });
    },

    // todo chỉ cho phép nhập số
    formatNumber(e) {
      var key = e.key;
      if (!/^\d+/g.test(key)) {
        e.preventDefault();
      }

      // if((this.asset.originalPrice == null ) && this.asset.wearValue == null)
      // {
      //   this.asset.originalPrice = "0";
      //   this.asset.wearValue  = "0"
      // }
      // if( this.asset.originalPrice == '')
      // {
      //   this.asset.originalPrice = "0";
      // }
      // if(this.asset.wearValue == '')
      // {
      //   this.asset.wearValue  = "0"
      // }

      // setTimeout(() => {
      //   if (
      //     parseInt(this.asset.originalPrice) <= parseInt(this.asset.wearValue)
      //   ) {
      //     this.asset.wearValue = null;
      //   }
      // }, 200);
    },

    //todo định dạng kiểu tiền tệ
    formatMoney(money) {
      return money.replace(/\B(?=(\d{3})+(?!\d))/g, `.`);
    },
    /**
     * ẩn hết những ngày sau ngày hiện tại
     * CreatedBy: TVHIEN (13/04/2021)
     */
    disabledAfterToday(date) {
      const today = new Date();
      today.setHours(0, 0, 0, 0);
      return date > today;
    },

    // todo bỏ định dạng tiền tệ
    removeFormatMoney(money) {
      return money.replace(/\D+/g, "");
    },

    //todo chuyển số thành chuỗi
    numberToString(input) {
      if (input == null) return "";
      else return input.toString();
    },

    // todo chuyển chuỗi thành số
    stringToNumber(input) {
      if (input == "" || input == "0") return null;
      else return parseInt(input);
    },

    // update input
    updateInput(text, e) {
      if (text == "wearValue")
        this.asset.wearValue = this.removeFormatMoney(e.target.value);
      else this.asset.originalPrice = this.removeFormatMoney(e.target.value);
    },

    // validate trống trường dữ liệu mã tài sản
    async validateAssetCode() {
      this.dup = false
      var warning = document.getElementById("assetInput1");
      if (this.asset.assetCode == null || this.asset.assetCode == "") {
        // warning.style.border = "1px solid red";
        warning.classList.add("borderRed");
        warning.classList.add("hover-validate");
      } else {
        warning.style.border = "#e4e4e4 1px solid";
        warning.classList.remove("hover-validate");
        warning.classList.remove("borderRed");
      }
    },

    // todo validate trường dữ liệu tên tài sản
    validateAssetName() {
      var warning = document.getElementById("assetInput2");
      if (this.asset.assetName == null || this.asset.assetName == "") {
        warning.classList.add("borderRed");
        warning.classList.add("hover-validate");
      } else {
        warning.style.border = "#e4e4e4 1px solid";
        warning.classList.remove("hover-validate");
        warning.classList.remove("borderRed");
      }
    },

    // todo lưu dữ liệu
    async save() {
      this.validateAssetName();
      this.validateAssetCode();
      if (
        parseInt(this.asset.originalPrice) <= parseInt(this.asset.wearValue)
      ) {
        document.getElementById("assetInput8").classList.add("borderRed");
        return;
      } else {
        document.getElementById("assetInput8").classList.remove("borderRed");
      }
      var res = this;
      if (res.asset.originalPrice == "") res.asset.originalPrice = null;
      if (res.asset.wearValue == "") res.asset.wearValue = null;

      if (
        this.asset.assetCode == null ||
        this.asset.assetName == null ||
        this.asset.assetCode == "" ||
        this.asset.assetName == ""
      ) {
        return;
      } else {
        if (this.formMode == "insert") {
          //nếu là form thêm dữ liệu
          await axios
            .post("https://localhost:44382/api/v1/assets/", this.asset)
            .then((respone) => {
              // nếu không gặp lỗi badrequest
              if (respone.data.errorCode == 400) {
                var warning = document.getElementById("assetInput1");
                res.dup = true
                warning.classList.add("borderRed");

                res.$emit("reload", false);
                return;
              } else if (
                respone.data.errorCode != 400 &&
                parseInt(res.asset.originalPrice) <=
                  parseInt(res.asset.wearValue)
              ) {
                res.dup =false
                return;
              } else {
                res.dup =false
                res.hide();
                res.$emit("reload", true);
              }
            })
            .catch((error) => {
              res.$emit("reload", false);
              console.log(error);
              alert("Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp");
            });
        } else {
          // nếu là form sửa dữ liệu

          await axios
            .put("https://localhost:44382/api/v1/assets/", this.asset)
            .then((respone) => {
              if (respone.data.errorCode != 400) {
                res.hide();
                res.$emit("reload", true);
              } else {
                 res.dup = true
                document
                  .getElementById("assetInput1")
                  .classList.add("borderRed");
                
                res.$emit("reload", false);
                return;
              }
            })
            .catch((error) => {
              console.log(error);
              res.$emit("reload", false);
              alert("Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp!");
            });
        }
      }
    },

    showWarning(text) {
      this.$emit("msgAlert", text, true);
    },
    checkPostedDate() {
      this.$emit("reload", false);
    },
  },
  watch: {
    // todo theo dõi id phòng ban để lấy ra tên phòng ban tương ứng
    "asset.departmentId": function () {
      if (this.asset.departmentId == null) this.asset.departmentName = null;
      else this.getDepartmentName();
    },

    // todo theo dõi mã loại tài sản để lấy ra tên loại tài sản tương ứng
    "asset.assetTypeId": function () {
      if (this.asset.assetTypeId == null) this.asset.assetTypeId = null;
      else this.getAssetTypeName();
    },
  },
  computed: {
    // todo nhận được kiểu định dạng tiền tệ khi nguyên giá thay đổi
    formatedMoney() {
      return this.formatMoney(this.numberToString(this.asset.originalPrice));
    },
    formatedWearValue() {
      return this.formatMoney(this.numberToString(this.asset.wearValue));
    },
  },
  filters: {},
  mounted()
  {
  this.selectAll()
  }
};
</script>


<style lang="scss" scoped>
input {
  padding: 8px 16px;
  outline: none;
  border: #beccc9 1px solid;
}

input.required {
  border: red 1px solid !important;
}

.input-search {
  width: 200px;
  padding: 8px 46px 8px 16px;
  border: 1px solid #beccc9;
}

.input-field .input-black {
  background-color: #f5f5f5;
  pointer-events: none;
}

.input-field .input-two-third {
  width: 437px;
}

.input-field .input-one-third {
  width: 210px;
}

.custom-autocomplete input {
  border: none;
  padding-left: 10px;
  font-size: 14px;
}

.v-text-field > .v-input__control > .v-input__slot:before {
  border-style: none;
}

.v-input {
  padding-top: 0px;
  margin-top: 0px;
}

.v-text-field__details {
  display: none;
}

.v-input__slot {
  margin-bottom: 0px;
}

.v-list-item__content .v-list-item__title {
  font-size: 14px;
}

// ::-webkit-scrollbar {
//   width: 8px;
//   height: 8px;
//  }

.test-autocomplete {
  border: 1px solid black;
}

.modal .validate-warning {
  font-family: GoogleSans-Regular;
  font-size: 13px;
  color: red;
  background-color: white;
  /* z-index: 22; */
  position: absolute;
}

.modal-content {
  position: absolute;
  top: calc(50% - 275px);
  left: calc(50% - 350px);
  width: 705px;
  height: 530px;
  background-color: white;
  resize: both;
  overflow: auto;
  min-height: 530px;
  min-width: 705px;
  height: 530px;
  width: 705px;

  .header {
    width: 100%;
    height: 60px;
    display: flex;
    padding: 0 16px;
    box-sizing: border-box;
    display: flex;
    align-items: center;

    .title {
      line-height: 50px;
      font-family: GoogleSans-Bold;
      font-size: 20px;
    }

    .header-right {
      position: absolute;
      right: 0;
      display: flex;
      align-items: center;
      padding: 0 12px;

      .icon-cancel {
        background-size: 16px;
        margin-left: 2px;
      }
    }
  }

  .content {
    width: 100%;
    height: calc(100% - 110px);
    padding: 16px 0px 16px 16px;
    box-sizing: border-box;
    overflow-y: auto;
    overflow-x: hidden;

    .input-field {
      float: left;
      padding: 0 16px 16px 0px;
      position: relative;
      height: 77px;

      label {
        display: block;
        padding: 2px 2px 4px 2px;
      }

      input {
        height: 34px;
        box-sizing: border-box;
        border: #e4e4e4 1px solid;
        // outline-color: lightgreen;
        padding: 10px;
      }
    }
  }

  .footer {
    width: 100%;
    height: 50px;
    background-color: #f5f5f5;
    display: flex;
    align-items: center;
    position: relative;
    padding: 0 16px;
  }
}

.validate-warning {
  display: none;
  position: absolute;
  font-style: italic;
  padding-right: 0;
}
.hover-validate:hover ~ .validate-warning {
  display: block;
}
.modal-content .header .title {
  font-family: GoogleSans-Bold !important;
}
.borderRed {
  border: 1px solid red !important;
}

#assetInput5 {
  text-align: right;
}
#assetInput6 {
  text-align: right;
}

input[type="date"] {
  opacity: 1;
  display: block;
  background: url(../../assets/icon/calendar.svg) no-repeat;
  width: 30px;
  height: 30px;
  border-width: thin;
  margin-top: 20px;
  transform: translateX(5px);
}
// @import url("../../style/scss/common.scss");
// @import url("../../style/scss/icon.scss");
// @import url("../../style/scss/button.scss");
// @import url("./../../style/scss/combobox.scss");
// @import url("../../style/scss/table.scss");
//  @import url("../../style/scss/date_picker.scss");
</style>