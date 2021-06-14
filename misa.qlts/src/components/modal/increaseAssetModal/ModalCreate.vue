<template>
  <div>
    <div v-show="isActive" class="modal">
      <div class="modal-background"></div>
      <div>
        <div v-show="isChange" class="div-container modal1">
          <div class="modal-background"></div>
          <div class="modal1-content">
            <div class="modal1-header">
              Dữ liệu chưa được lưu
              <span @click="hide('', 'continue')"
                ><div data-v-1b34bf39="" class="icon-cancel btn btn-close"></div
              ></span>
            </div>
            <div class="modal1-content" style="width: 318px">
              Dữ liệu thay đổi, bạn có muốn lưu không? <br />
            </div>
            <div class="modal1-footer">
              <div
                style="
                  width: 86px;
                  justify-content: center;
                  background: #1ba3c9;
                "
                class="btn btn-save-fake"
                @click="save()"
              >
                Lưu
              </div>
              <div
                style="
                  width: 86px;
                  justify-content: center;
                  border: 1px solid #1ba3c9;
                "
                class="btn"
                @click="hide()"
              >
                Không lưu
              </div>
              <div
                style="width: 86px; justify-content: center"
                class="btn"
                @click="hide('', 'continue')"
              >
                Hủy bỏ
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-content">
        <div class="header">
          <div v-if="formMode == 'insert'" class="title">
            Thêm chứng từ ghi tăng
          </div>
          <div v-else class="title">Sửa chứng từ ghi tăng</div>

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
          <div class="modal-container input-container container-top">
            <div class="top-label">
              <label for="">Thông tin chứng từ </label>
            </div>
            <div class="container-box input-box">
              <div class="input-row">
                <div class="input-field asset-code">
                  <label for=""
                    >Mã chứng từ <span style="color: red">(*)</span></label
                  >
                  <input
                    placeholder="Nhập mã chứng từ"
                    onClick="this.select();"
                    id="assetInput1"
                    type="text"
                    maxlength="20"
                    class="required input-one-third"
                    v-model="assetIncrease.exhibitCode"
                    ref="exhibitCode"
                    @blur="validateEmty('exhibitCode')"
                    @keyup="validateEmty('exhibitCode')"
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
                    Mã chứng từ bị trùng
                  </div>
                </div>
                <div class="input-field date-proc">
                  <label for=""
                    >Ngày chứng từ <span style="color: red">(*)</span></label
                  >
                  <DatePicker
                    class="required input-sm"
                    input-class="datetime"
                    placeholder="dd/MM/yyyy"
                    v-model="assetIncrease.exhibitDate"
                    type="date"
                    ref="exhibitDate"
                    @blur="validateEmty('exhibitDate')"
                    format="DD/MM/YYYY"
                    value-type="YYYY-MM-DD"
                    :disabled-date="disabledAfterToday"
                    @input-error="showWarning('Ngày sai định dạng', true)"
                  >
                    <template v-slot:header="{ emit }">
                      <button
                        class="mx-btn mx-btn-text"
                        @click="emit(new Date())"
                      >
                        Hôm nay
                      </button>
                    </template>
                  </DatePicker>
                  <div class="validate-warning">Thông tin bắt buộc nhập</div>
                </div>
                <div class="input-field date-increase">
                  <label for=""
                    >Ngày ghi tăng <span style="color: red">(*)</span></label
                  >
                  <DatePicker
                    class="required input-sm"
                    input-class="datetime"
                    placeholder="dd/MM/yyyy"
                    v-model="assetIncrease.increaseDate"
                    type="date"
                    format="DD/MM/YYYY"
                    value-type="YYYY-MM-DD"
                    :disabled-date="disabledAfterToday"
                    @input-error="showWarning('Ngày sai định dạng', true)"
                    ref="increaseDate"
                    @blur="validateEmty('increaseDate')"
                  >
                    <template v-slot:header="{ emit }">
                      <button
                        class="mx-btn mx-btn-text"
                        @click="emit(new Date())"
                      >
                        Hôm nay
                      </button>
                    </template>
                  </DatePicker>
                  <div class="validate-warning">Thông tin bắt buộc nhập</div>
                </div>
              </div>
              <div class="input-row">
                <div class="input-field field-note">
                  <label for="">Ghi chú</label>
                  <input
                    placeholder="Nhập ghi chú"
                    onClick="this.select();"
                    id="assetInput1"
                    type="text"
                    maxlength="40"
                    class="input-one-third"
                    v-model="assetIncrease.note"
                  />
                </div>
              </div>
            </div>

            <div id="assetInput1_warning" class="validate-warning">
              Thông tin bắt buộc nhập
            </div>
          </div>

          <div class="modal-container container-below">
            <div class="below-label">
              <label for="">Thông tin tài sản ghi tăng </label>
            </div>
            <div class="container-box grid-box">
              <div class="panel-header">
                <div class="features-pane">
                  <div class="features-pane-left">
                    <input
                      v-model="inputSearch"
                      id="assetSearchBox"
                      class="input-search"
                      type="text"
                      placeholder="Tìm kiếm. "
                    />
                    <div class="icon-search"></div>
                  </div>

                  <div class="features-pane-right">
                    <div
                      @click="chooseAsset()"
                      id="add-asset"
                      class="btn features-pane-item"
                    >
                      <div class="icon-add">
                        <div class="icon"></div>
                      </div>
                      <div class="text-add">
                        <div class="text">Chọn tài sản</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="content-grid grid">
                <table class="table-asset" id="tableAsset">
                  <colgroup>
                    <col width="0px" />
                    <col width="0px" />
                    <col min-width="0px" />
                    <col min-width="0px" />
                    <col min-width="0px" />
                    <col width="0px" />
                    <col width="0px" />
                    <col width="0" />
                  </colgroup>
                  <thead>
                    <tr>
                      <th style="text-align: left">STT</th>
                      <th
                        sortProp="code"
                        sortOrder="asc"
                        id="columnAssetCode"
                        class="hover-pointer"
                        style="text-align: left"
                      >
                        mã tài sản
                      </th>
                      <th
                        sortProp="name"
                        sortOrder="asc"
                        id="columnAssetName"
                        class="hover-pointer"
                        style="text-align: left"
                      >
                        Tên tài sản
                      </th>
                      <th
                        sortProp="department"
                        sortOrder="asc"
                        id="columnDepartment"
                        class="hover-pointer"
                        style="text-align: left"
                      >
                        Bộ phận sử dụng
                      </th>
                      <th
                        sortProp="price"
                        sortOrder="asc"
                        id="columnPrice"
                        class="hover-pointer"
                        style="text-align: right"
                      >
                        nguyên giá
                      </th>
                      <th class="hover-pointer" style="text-align: right">
                        HM/KM lũy kế
                      </th>
                      <th class="hover-pointer" style="text-align: right">
                        Giá trị còn lại
                      </th>
                      <th></th>
                    </tr>
                  </thead>

                  <tbody>
                    <tr
                      v-for="(item, index) in listAssetView"
                      :key="index"
                      v-show="
                        item.assetCode
                          .toLocaleLowerCase()
                          .indexOf(inputSearch.toLocaleLowerCase()) !== -1 ||
                        item.assetName
                          .toLocaleLowerCase()
                          .indexOf(inputSearch.toLocaleLowerCase()) !== -1
                      "
                    >
                      <td class="no-border-left">{{ index + 1 }}</td>
                      <td
                        :title="item.assetCode"
                        style="text-overflow: ellipsis"
                      >
                        {{ item.assetCode }}
                      </td>
                      <td
                        :title="item.assetName"
                        style="text-overflow: ellipsis"
                      >
                        {{ item.assetName }}
                      </td>
                      <td
                        :title="item.departmentName"
                        style="text-overflow: ellipsis"
                      >
                        {{ item.departmentName }}
                      </td>
                      <td style="text-align: right; text-overflow: unset">
                        <input
                          class="inputMoney"
                          style="
                            width: 114px;
                            text-align: right;
                            text-overflow: ellipsis;
                          "
                          type="text"
                          v-model="item.originalPrice"
                          :title="item.originalPrice > item.wearValue? item.originalPrice :'Yêu cầu nguyên giá lớn hơn lũy kế'"
                          v-money="money"
                          maxlength="20"
                          @keyup="updateResidual(item, $event, 'origin')"
                          onClick="this.select();"
                          @keypress="onlyNumber($event)"
                        />
                      </td>
                      <td style="text-align: right; text-overflow: unset">
                        <input
                          style="
                            width: 103px;
                            text-align: right;
                            text-overflow: ellipsis;
                          "
                          type="text"
                          onClick="this.select();"
                          class="inputMoney"
                          v-model="item.wearValue"
                          :title="item.originalPrice > item.wearValue? item.wearValue :'Yêu cầu nguyên giá lớn hơn lũy kế'"
                          v-money="money"
                          maxlength="20"
                          @keyup="updateResidual(item, $event, 'wear')"
                           @keypress="onlyNumber($event)"
                        />
                      </td>

                      <td
                        class="res-value no-border-right"
                        style="text-align: right; text-overflow: ellipsis"
                        :title="item.resValue | formatMoney(item.resValue)"
                      >
                        {{ item.resValue | formatMoney(item.resValue) }}
                      </td>
                      <td>
                        <div
                          @click="deleteItem(item.assetId)"
                          data-v-bdaea12c=""
                          class="icon-refresh-time1 btn btn-confirm-delete1"
                          title="Xóa"
                        ></div>
                      </td>
                    </tr>
                    <div v-show="listAssetView.length == 0" class="emty-text">
                      Không có dữ liệu tài sản ghi tăng
                    </div>
                  </tbody>

                  <!-- <div v-show="getEmty" class="loading-emty">Không có dữ liệu</div> -->
                </table>

                <div class="ctx-menu" id="ctxMenu">
                  <div class="ctx-menu-item" @click="showDialog('insert', 0)">
                    Thêm
                  </div>
                  <div
                    class="ctx-menu-item"
                    @click="showDialog('update', listSelectRow[0])"
                  >
                    Sửa
                  </div>
                  <div
                    id="preventLeftClick"
                    class="ctx-menu-item"
                    @click="showDeleteDialog(listSelectRow[0])"
                  >
                    Xóa
                  </div>
                </div>
              </div>
              <div class="sumary">
                <div class="sumary-text">
                  Tổng số: {{ listAssetView.length }} tài sản
                </div>
              </div>
            </div>

            <div id="assetInput1_warning" class="validate-warning">
              Thông tin bắt buộc nhập
            </div>
          </div>
        </div>
        <div class="footer">
          <div class="btn btn-cancel" tabindex="0" @click="hide('close')">
            Hủy
          </div>

          <div class="btn btn-save" tabindex="0" @click="save('all')">Lưu</div>
        </div>
      </div>
      <BaseLoading ref="BaseLoading_ref" />
      <AssetManagement
        :listAssetView="listAssetView"
        @listAssetView="getAssetView"
        ref="chooseAsset_ref"
      />
    </div>
  </div>
</template>

<script src="https://unpkg.com/vuejs-datepicker/dist/locale/translations/fr.js"></script>

<script>
import axios from "axios";

//  as a directive
import { VueMaskDirective } from "v-mask";

import "vue2-datepicker/locale/vi";

import DatePicker from "vue2-datepicker";
import "vue2-datepicker/index.css";

import AssetManagement from "./ModalChooseAsset.vue";
import BaseLoading from "../../common/BaseLoading.vue";
import { VMoney } from "v-money";

export default {
  components: {
    DatePicker,
    AssetManagement,
    BaseLoading,
  },
  props: {
    listAssetType: Array,
    listDepartment: Array,
    formMode: String,
    assetIdUpdate: String,
  },
  directives: { money: VMoney, mask: VueMaskDirective },
  data() {
    return {
      money: {
        decimal: ".",
        thousands: ".",
        prefix: "",
        suffix: "",
        precision: 0,
        masked: false /* doesn't work with directive */,
      },
      isActive: false,
      showSuccess: true,
      isChoose: false,
      listAssetView: [], // danh sách tài sản trên khung nhìn
      assetIncrease: {
        increaseId: null,
        exhibitCode: null,
        exhibitDate: null,
        increaseDate: null,
        note: null,
        increaseDetail: null,
        createdBy: null,
        modifiedBy: null,
        createdDate: null,
        modifiedDate: null,
      },
      inputSearch: "",

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
      indexUpdateAsset: null, // lưu lại vị trí tài sản đang sửa
      listResPrice: [],
      assetIncreaseCompare: "",
      isChange: false,
      validInputMoney: true,
    };
  },

  methods: {
    //TODO chỉ cho phép nhập số
    onlyNumber(e) {
      var trapNumber = /[0-9\/]+/;
      if (!trapNumber.test(e.key)) {
        e.preventDefault();
        return;
      }
    },

    
    // todo cập nhật giá trị còn lại
    updateResidual(item, e, text) {
      typeof item.originalPrice === "string"
        ? (item.originalPrice = this.fomatMoneyToNumber(item.originalPrice))
        : item.originalPrice;
      typeof item.wearValue === "string"
        ? (item.wearValue = this.fomatMoneyToNumber(item.wearValue))
        : item.wearValue;

      item.resValue = item.originalPrice - item.wearValue;

      if (e != "view") {
        if (text == "origin") {
          e.target.parentElement.nextElementSibling.firstElementChild.classList.remove('alert')
          if (item.resValue < 0) {
            this.validInputMoney = false
            e.target.classList.add("alert");
          } else {
            this.validInputMoney = true
            e.target.classList.remove("alert");
          }
        } else if (text == "wear") {
          e.target.parentElement.previousElementSibling.firstElementChild.classList.remove('alert')
          if (item.resValue < 0) {
            this.validInputMoney = false
            const number = /[0-9\/]+/;
            
            e.target.classList.add("alert");
          } else {
            this.validInputMoney = true
            e.target.classList.remove("alert");
          }
        }
      } else {
      }
    },
    //todo thêm border khi click vào input money
    addBorder(e) {
      e.target.style.border = "1px solid black";
    },
    //todo xóa border khi blur khỏi input money
    removeBorder(e) {
      e.target.style.border = "1px solid white";
    },
    // todo hiển thị dialog chọn tài sản
    chooseAsset() {
      this.$refs.chooseAsset_ref.show();
    },

    //todo reset lại input
    resetInput() {
      for (var key in this.assetIncrease) {
        this.assetIncrease[key] = null;
      }
      this.listAssetView = [];
      this.turnOffValidate();
    },

    //todo tắt các validate
    turnOffValidate() {
      document.getElementsByClassName("required").forEach((element) => {
        element.style.border = "1px solid #e4e4e4";
        element.classList.remove("hover-validate");
        document.getElementsByClassName('inputMoney').forEach(el => {
          el.classList.remove('alert')
        })
      });
      this.dup = false;

      // debugger; // eslint-disable-line no-debugger
    },
    // todo hiển thị dialog thêm chứng từ ghi tăng
    async show() {
      this.turnOffValidate();
      this.inputSearch = ''
      var res = this;
      this.isActive = true;
      this.dup = false;
      this.validInputMoney = true;

      var warning = document.getElementById("assetInput1");

      //warning.classList.remove("hover-validate");

      // document.getElementById("assetInput1_warning").style.display = "none"

      // focus vào input đầu tiên( mã tài sản)

      setTimeout(() => {
        document.getElementById("assetInput1").focus();
      }, 0);
      //  document.getElementById("assetInput1").focus();

      document.getElementsByClassName("body-right")[0].style.zIndex = "999";
      if (this.formMode == "update") {
        await axios
          .get(
            "https://localhost:44382/api/v1/AssetIncreases/" +
              this.assetIdUpdate
          )
          .then((Response) => {
            res.assetIncrease = Response.data.data;
            res.listAssetView = JSON.parse(res.assetIncrease.increaseDetail);
            res.assetIncreaseCompare = JSON.stringify(res.assetIncrease);

            // debugger; // eslint-disable-line no-debugger
          })
          .catch((error) => {
            this.errorMessage = error.message;
            console.error("GET Asset by id Failed: ", error.message);
          });

        // convert ngyên giá sang string
        // var a = res.asset.originalPrice;
        // if (a == null) a = "";
        // else a = res.asset.originalPrice.toString();

        // res.asset.originalPrice = a;
        //debugger; // eslint-disable-line no-debugger
      } else {
        this.resetInput();
        this.createdDateRender();
      }
    },

    // todo ẩn dialog
    hide(text1, text2) {
      if (this.formMode == "update" && text1 == "close") {
        if (
          this.assetIncreaseCompare != JSON.stringify(this.assetIncrease) ||
          JSON.stringify(this.listAssetView) !=
            this.assetIncrease.increaseDetail
        ) {
          this.isChange = true;
        } else {
          this.isChange = false;
          this.isActive = false;
          return;
        }
      } else if (text2 == "continue") {
        this.isChange = false;
        return;
      } else {
        this.isChange = false;
        this.isActive = false;
        document.getElementsByClassName("body-right")[0].style.zIndex = "0";
        this.assetIncrease.increaseDate = null;
        this.assetIncrease.exhibitDate = null;
        this.$emit("statusForm", "");
      }
    },

    // todo select tất cả nội dung ô input khi click
    selectAll() {
      document.getElementsByTagName("input").forEach((element) => {
        element.addEventListener("click", () => {
          element.select();
        });
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
    updateInput(text, e, index) {
      this.indexUpdateAsset = index;
      if (text == "wearValue")
        this.asset.wearValue = this.removeFormatMoney(e.target.value);
      else if (text == "originPrice") {
        this.listAssetView[index].originalPrice = this.removeFormatMoney(
          e.target.value
        );
      }
    },

    // todo chuyền thông báo cảnh báo
    showWarning(text) {
      this.$emit("msgAlert", text, true);
    },

    // định dạng ngày
    formatDate() {},
    //todo kiểm tra ngày tạo
    checkPostedDate() {
      this.$emit("reload", false);
    },
    // todo nhận dữ liệu danh sách tài sản
    getAssetView(listId) {
      var res = this;
      var i = 0;
      res.$refs.BaseLoading_ref.show();
      listId.forEach((element) => {
        axios
          .get("https://localhost:44382/api/v1/Assets/" + element)
          .then((Response) => {
            var dat = Response.data.data;
            res.$set(dat, "resValue", 0);
            this.updateResidual(dat, "view");
            res.listAssetView.push(dat);

            i++;
            if (i == listId.length) {
              res.$refs.BaseLoading_ref.hide();
            }
            //debugger; // eslint-disable-line no-debugger
          })
          .catch((error) => {
            this.errorMessage = error.message;
            console.error("GET Asset by id Failed: ", error.message);
          });
      });
    },
    //todo xóa 1 dòng dữ liệu tài sản ghi tăng
    deleteItem(id) {
      var res = this;
      this.listAssetView.forEach((element, index) => {
        if (element.assetId == id) {
          res.listAssetView.splice(index, 1);
          res.listResPrice.splice(index, 1);
        }
      });
    },

    // todo đổi tiền tệ kiểu string về số
    fomatMoneyToNumber(value) {
      if (value != "") {
        return parseFloat(value.split(".").join(""));
      }
      return 0;
    },
    //Todo lưu tài sản
    async save() {
      var res = this;

      //  validate lại các trường yêu cầu nhập
      this.validateEmty("exhibitCode");
      this.validateEmty("exhibitDate");
      this.validateEmty("increaseDate");

      // sinh ra createdDate và modifiedDate

      if (
        this.assetIncrease.exhibitCode == null ||
        this.assetIncrease.exhibitDate == null ||
        this.assetIncrease.increaseDate == null 
      ) {
        return;
      }
      else if(
        this.validInputMoney == false)
        {
          this.showWarning("Nguyên giá phải lớn hơn lũy kế")
        }
      else {
        // check đã chọn tài tải chưa
        if (this.listAssetView.length == 0) {
          this.showWarning("Vui lòng chọn tài sản ");
          return;
        }
        //todo cập nhập lại danh sách các tài sản ghi tăng theo dạng text
        this.assetIncrease.increaseDetail = JSON.stringify(this.listAssetView);
        this.assetIncrease.exhibitCode = this.assetIncrease.exhibitCode.replace(
          /^\s+|\s+$/gm,
          ""
        );
        if (this.formMode == "insert") {
          //nếu là form thêm dữ liệu
          await axios
            .post(
              "https://localhost:44382/api/v1/assetIncreases/",
              res.assetIncrease
            )
            .then((respone) => {
              // nếu không gặp lỗi badrequest
              if (respone.data.errorCode == 400) {
                var warning = document.getElementById("assetInput1");
                res.dup = true;
                warning.style.border = "1px solid red";

                res.$emit("reload", false);
                return;
              } else {
                res.dup = false;
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
            .put(
              "https://localhost:44382/api/v1/assetIncreases/",
              this.assetIncrease
            )
            .then((respone) => {
              if (respone.data.errorCode != 400) {
                res.hide();
                res.$emit("reload", true);
              } else {
                res.dup = true;

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

    // validate trống trường dữ liệu
    async validateEmty(text) {
      this.dup = false;
      if (text != "all") {
        if (
          this.assetIncrease[text] == null ||
          this.assetIncrease[text] == ""
        ) {
          if (text == "exhibitCode") {
            this.$refs[text].style.border = "1px solid red";
            this.$refs[text].classList.add("hover-validate");
          } else {
            this.$refs[text].$el.style.border = "1px solid red";
            this.$refs[text].$el.classList.add("hover-validate");
          }
        } else {
          if (text == "exhibitCode") {
            this.$refs[text].style.border = "1px solid #e4e4e4";
            this.$refs[text].classList.remove("hover-validate");
          } else {
            this.$refs[text].$el.style.border = "1px solid #e4e4e4";
            this.$refs[text].$el.classList.remove("hover-validate");
          }
        }
        //debugger; // eslint-disable-line no-debugger
      }
    },

    // todo khởi tạo ngày tạo bản ghi
    createdDateRender() {
      var a = new Date();
      var month = a.getMonth();
      var day = a.getDate();
      if (month < 10) month = "0" + month.toString();
      if (day < 10) day = "0" + day.toString();
      var date = a.getFullYear().toString() + "-" + month + "-" + day;
      this.assetIncrease.createdDate = date;
      this.assetIncrease.modifiedDate = date;
      // debugger; // eslint-disable-line no-debugger
    },
  },
  filters: {
    // todo định dạng kiểu tiền tệ cho nguyxên giá
    formatMoney: function (money) {
      if (money != null) {
        var num = money.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
      } else return "0";
      return num;
    },
  },
  watch: {
    // "assetIncrease.increaseDetail.originalPrice": function () {
    //   if (this.assetIncrease.increaseDetail == null) {
    //     this.listAssetView = [];
    //   } else {
    //     this.listAssetView = JSON.parse(this.assetIncrease.increaseDetail);
    //   }
    // },

    "assetIncrease.exhibitDate": function () {
      this.validateEmty("exhibitDate");
    },
    "assetIncrease.increaseDate": function () {
      this.validateEmty("increaseDate");
    },
  },

  created() {
    this.createdDateRender();
  },
  mounted() {
    this.selectAll();
  },
};
</script>





<style lang="scss" scoped>
input {
  padding: 8px 16px;
  outline: none;
  border: #beccc9 1px solid;
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

#assetInput5 {
  text-align: right;
}
#assetInput6 {
  text-align: right;
}

.modal-content {
  width: 880px;
}
.modal-container {
  width: 100%;
  height: 50%;
}
.modal-content .header {
  border-bottom: 1px solid;
  background: white;
}
.modal-content {
  background: #f5f6fa;
}
.container-box {
  height: 100%;
}

.below-label {
  height: 34px;
}
.top-label {
  height: 34px;
}
.container-box {
  background: white;
}
.container-below {
  box-sizing: border-box;
}
.container-top {
  padding-bottom: 40px;
}
.modal-content .content .input-field input {
  border-radius: 2px;
}
#assetInput1 {
  width: 100%;
}
.input-row .asset-code {
  width: 50%;
}
.date-proc,
.date-increase {
  width: 25%;
}
.panel-header .features-pane {
  display: flex;
}
.features-pane-left {
  position: relative;
}
.icon-search {
  position: absolute;
  left: 0;
  top: 5;
}
.input-search {
  width: 200px;
  padding: 8px 46px 8px 38px;
  border: 1px solid #beccc9;
}
.features-pane-right {
  position: absolute;
  right: 24px;
}
.features-pane-right #add-asset .icon {
  background: red;
}
.features-pane-right #add-asset {
  width: 150px;
}

table td,
th {
  white-space: nowrap;
}
.modal-content .header {
  border-bottom: 1px solid #e4e4e4;
  background: white;
}
.modal-content {
  height: 578px;
}
.container-box {
  background: white;
  box-shadow: 0 0 7px #bbb6b6;
  border: 1px solid #e4e4e4;
}
.container-below {
  box-sizing: border-box;
  overflow: hidden;
}
.modal-content .footer {
  border-top: 2px solid #eaeaea;
}
.container-box.grid-box {
  height: calc(100% - 34px);
}
.container-box {
  padding: 16px;
}

.modal-container {
  width: 100%;
  height: 45%;
}
.container-box {
  height: calc(100% - 34px);
}
.container-top {
  /* padding-bottom: 40px; */
  padding-bottom: 0px;
}
.modal-content .content .field-note {
  width: 100%;
}
.modal-content .content .input-field {
  padding: 0 0px 16px 0px;
}
.modal-content .content {
  padding: 16px;
}
.input-field.asset-code {
  box-sizing: border-box;
}
.input-field.date-proc {
  box-sizing: border-box;
}
.input-field.date-proc {
  padding-left: 16px !important;
}
.input-field.date-increase {
  padding-left: 16px !important;
}
::placeholder {
  color: rgb(191, 194, 191);
  font-size: 12px;
}
.modal-container.container-below {
  height: 55%;
  padding-top: 30px;
}
input#assetSearchBox {
  height: 34px;
  border-radius: 2px;
}
.icon-search {
  position: absolute;
  left: 0;
  top: 3px;
}
input#assetSearchBox {
  height: 34px;
  width: 246px;
  border-radius: 2px;
}
.features-pane-right #add-asset {
  width: 150px;
  border: 1px solid #beccc9;
}
.icon-add {
  height: 25px;
  width: 30px;
  background: url(../../../assets/icon/qlts-icon.svg) no-repeat -196px -416px;

  background-repeat: no-repeat;
}
.features-pane-right #add-asset:hover {
  background-color: rgb(221 221 221);
}
.features-pane-right #add-asset {
  width: 150px;
  border: 1px solid #beccc9;
  white-space: nowrap;
  display: flex;
  align-items: center;
}

.features-pane-right #add-asset[data-v-654548ea] {
  padding: 0 10px !important;
}
.features-pane-right #add-asset[data-v-654548ea] {
  width: auto;
}
.features-pane-right[data-v-654548ea] {
  position: absolute;
  right: 34px;
}
.modal-content {
  top: calc((100% - 632px) / 2) !important;
  left: calc((100% - 880px) / 2) !important;
  height: 632px !important;
}
.panel-header {
  padding-bottom: 20px;
}


.panel-header[data-v-654548ea] {
  /* padding-bottom: 20px; */
  padding: 16px;
  padding-bottom: 20px;
}
.container-box[data-v-654548ea] {
  padding: 0px;
}
table thead th,
td {
  border-right: none;
}
.container-box.input-box {
  padding: 16px !important;
}
.panel-header {
  /* padding-bottom: 20px; */
  padding: 16px;
  /* padding-bottom: 20px; */
  padding-bottom: none;
}
.modal-container {
  width: 100%;
  height: 42%;
}

.footer-grid {
  display: flex;
  align-items: center;
  padding-top: 11px;
  height: auto;
}
.footer-content {
  height: 34px;
  display: flex;
  align-items: center;
  border-top: 1px solid;
  width: 100%;
}
.modal-container.container-below {
  height: 58%;
  padding-top: 22px;
}
.footer-content {
  height: 34px;
  display: flex;
  align-items: center;
  border-top: 1px solid #d6d6d6;
  width: 100%;
}
.footer-content {
  padding-left: 16px;
}
.btn.btn-cancel {
  position: absolute;
  right: 113px;
}

.btn-confirm-delete1 {
  background-color: #ff4646;
  color: white;
  margin-left: 23px;
  height: 28px;
  box-sizing: border-box;
  padding: 0 13px !important;
  width: 50px;
}
table tr {
  cursor: pointer;
}
table th {
  cursor: default;
}
.container-box[data-v-654548ea][data-v-654548ea] {
  padding: 0px;
  position: relative;
}
.footer-content {
  padding-left: 16px;
  position: absolute;
  bottom: 0;
}
// tbody {
//     display: block;
//     overflow: auto;
//     position: relative;
//     height: 90px!important;
// }
// thead{
//   display: block;
//   width: 100%;
//  }

// table thead th:nth-child(1)
// {
// width: 2%;
// }
// table thead th:nth-child(2), td:nth-child(2)
// {
//   width: 10%;
// }
// table thead th:nth-child(3), td:nth-child(3)
// {
//   width: 21%;
// }
// table thead th:nth-child(4), td:nth-child(4)
// {
//   width: 16%;
// }
// table thead th:nth-child(5), td:nth-child(5)
// {
//   width: 13%;
//   text-align: right;
// }
// table thead th:nth-child(6), td:nth-child(6)
// {
//   width: 10%;

// }
// table thead th:nth-child(7), td:nth-child(7)
// {
//   width: 13%;
// }
//thinhj
#assetPopup {
  padding: 0 40px;
  height: 80px;
  background-color: #b1fdb3;
  color: black;
  font-size: 16px;
  border-left: 10px solid #e2f2ff;
  display: flex;
  padding-left: 26px;
  align-items: center;
  position: absolute;
  right: -500px;
  bottom: 80px;
  transition: right 1s;
}
#ctxMenu {
  background-color: white;
  position: fixed;
  z-index: 100;
  // display: none;
}

.ctx-menu {
  font-size: 14px;
  box-shadow: 2px 2px 4px grey;
  display: none;

  .ctx-menu-item {
    padding: 8px 34px 8px 18px;

    &:hover {
      background-color: #5973b3;
      cursor: pointer;
    }
  }
}
//TODO DATETIME theo định dạng d/m/y, bỏ chọn thời gian
//TODO Sử dụng được phím tab trong nhập liệu
//TODO Thay đổi font chữ của datetimepicker
//TODO Thêm khách hàng thành công;
#loadBar {
  width: 0;
  height: 3px;
  background-color: #00abfe;
  transition: 0.5s;
  margin-top: 8px;
  display: none;
  position: absolute;
}

// .content-nav {
//     position: relative;
// }

// .content-nav  :after {
//     content: '';
//     display: block;
//     border-bottom: 3px solid #00abfe;
//     width: 0;
//     position: absolute;
//     left: 0;
//     -webkit-transition: 1s ease;
//     transition: 1s ease;
// }

// .content-nav :hover:after {
//     width: 100%;
// }

.content-nav {
  height: 70px;
  padding: 16px;
  box-sizing: border-box;

  .features-pane {
    display: flex;
    position: relative;
    align-items: center;
    margin: 10px 0;
    // width: 100%;
    padding-right: 10px;
    font-size: 14px;

    input {
      height: 34px;
      width: 320px;
      box-sizing: border-box;
    }
    .btn {
      height: 34px;
    }

    .btn-search {
      margin-left: 10px;
    }

    .features-pane-right {
      position: absolute;
      right: 0;

      // background-color:lightsalmon;
      display: flex;
      .features-pane-item {
        margin: 0px 5px;
      }
    }

    .features-pane-left {
      display: flex;
      align-items: center;
      .icon-search {
        position: relative;
        right: 45px;

        &:hover {
          cursor: pointer;
          //TODO sau này thêm action tìm kiếm vào đây
        }
      }
    }
  }
}

.content-grid {
  margin: 16px 16px 16px 16px;
  height: calc(100% - 149px);
  position: relative;
  box-sizing: border-box;
}

.not-active {
  display: none;
}

.features-box {
  display: flex;
  align-items: center;
  visibility: hidden;
  .table-icon {
    width: 15px;
    height: 15px;
    margin: 0 4px;
    &:hover {
      cursor: pointer;
    }
  }
}

.table-summary {
  font-size: 14px;
  font-family: GoogleSans-Medium;
  display: flex;
  position: relative;
  margin: 18px 16px 0 16px;
  height: 45px;

  .price-number {
    position: absolute;
    //TODO sẽ phải sửa lại cái này cho chuẩn với cột nguyên giá
    right: 120px;
  }
}
.content {
  height: calc(100vh - 60px);
  width: calc(100% - 200px);
  margin-left: 210px;
  transition: all 0.25s;
  background-color: white;
  user-select: none;
}

.content-nav {
  padding-top: 26px;
  padding-bottom: 6px;
}
.content-nav .features-pane {
  margin: 0px;
}
.div-container {
  position: relative;
}
.table-summary {
  font-size: 14px;
  font-family: GoogleSans-Medium;
  display: flex;
  /* position: relative; */
  position: absolute;
  bottom: 0px;
  /* margin: 18px 16px 0 16px; */
  height: 63px;
}
.content {
  height: calc(100vh - 68px);
  width: calc(100% - 200px);
  margin-left: 210px;
  transition: all 0.25s;
}

.content-grid {
  margin: 16px 16px 16px 16px;
  height: calc(100% - 165px);
  /* height: 100%; */
  position: relative;
  overflow: auto;
  box-sizing: border-box;
}
.table-summary {
  font-size: 14px;
  font-family: GoogleSans-Medium;
  display: flex;
  /* position: relative; */
  width: 100%;
  position: absolute;
  bottom: 0px;
  /* align-items: center; */
  /* margin: 18px 16px 0 16px; */
  height: 63px;
  padding: 18px 16px;
  box-sizing: border-box;
}
.table-summary .summary {
  height: 100%;
  width: 100%;
  display: flex;
  align-items: center;
}

.content .div-container {
  height: 100%;
}

.content {
  position: relative;
  margin-left: 0;
  box-sizing: border-box;
  width: 100%;
  height: calc(100% - 60px);
}

table tr th {
  white-space: nowrap;
  cursor: auto !important;
}

table tbody tr {
  cursor: pointer;
}

.v-sheet.v-alert {
  position: absolute;
  z-index: 2;
  right: 0px;

  animation-name: alert;
  width: 220px;
  animation-duration: 3s;
  white-space: nowrap;
  padding: 16px 0px;
  bottom: 0px;
}

@keyframes alert {
  0% {
    width: 220px;
    padding: 16px;
  }
  50% {
    width: 220px;
    padding: 16px;
  }
  100% {
    width: 0px;
    padding: 16px 0px;
  }
}


.summary {
  display: flex;
}

#ctxMenu {
  position: relative;
  width: 152px;
  position: fixed !important;
}
#required-choose {
  display: block !important;
  margin-right: 8px;
}

.text-pagebumber::-webkit-outer-spin-button,
.text-pagebumber::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

.btn-move-page {
  border-radius: 4px;
}

.p-number,
.btn-move-page {
  height: 34px;
  width: 34px;
  margin: 0 8px 0 8px;
  text-align: center;
  background-repeat: no-repeat;
  background-size: contain;
  background-size: 24px;
  background-position: center;
}

.p-number:hover,
.btn-move-page:hover {
  background-color: #bbbbbb;
  cursor: pointer;
}

.p-number {
  border-radius: 50%;
  border: 1px solid #bbbbbb;
  line-height: 34px;
}

.btn-first-page {
  background-image: url("../../../assets/icon/btn-firstpage.svg");
}

.prev-page {
  background: url(../../../assets/icon/common-icon.png) no-repeat -700px -124px;
}

.paging-toolbar {
  height: 100%;
  display: flex;
  margin-left: calc((100% - 752px) / 2);
}

.paging-toolbar .leftchild {
  height: 100%;
  min-width: 330px;
  display: flex;
  align-items: center;
}

.paging-toolbar .leftchild .p-button {
  height: 24px;
  width: 24px;
  border: 1px solid #d0d0d0;
  border-radius: 3px;
  background-color: #fff;
  margin-right: 2px;
}

.paging-toolbar .leftchild .first-page {
  /* background: url(/QLCH/resources/images/common-icon.png) no-repeat -649px -124px;
    background: url(../../icon/common-icon.png) no-repeat;
    background-repeat: no-repeat;
    background-size: contain;
    background-position: center; */
  background: url(../../../assets/icon/common-icon.png) no-repeat -649px -124px;
  width: 24px;
  height: 24px;
  background-color: #fff;
  border-radius: 3px;
  cursor: pointer;
}

.text-pagebumber,
.select-quantitypage {
  border-radius: 3px;
  text-align: center;
}

.text-pagebumber {
  width: 36px;
  height: 16px;
  padding: 4px 6px;
  border: 1px solid #d0d0d0;
  box-sizing: border-box;
  height: 24px;
}

.refresh {
  background: url(../../../assets/icon/common-icon.png) no-repeat -849px -124px;
  border: 1px solid #6b6f9d !important;
}

.leftchild .last-page {
  background: url(../../../assets/icon/common-icon.png) no-repeat -798px -124px;
}

.leftchild .next-page {
  background: url(../../../assets/icon/common-icon.png) no-repeat -749px -124px;
}
.select-quantitypage {
  width: 46px;
  height: 26px;
  border: 1px solid #d0d0d0;
  outline: none !important;
}
.text-pagebumber,
.select-quantitypage {
  border-radius: 3px;
  text-align: center;
}

.select-quantitypage {
  appearance: none;
  background: url(../../../assets/icon/arrow-down-line.png) no-repeat;
  background-position: 29px 11px;
  padding-left: 4px;
}

.filter-menu {
  height: 80px;
  width: 102px;
  display: flex;
  flex-direction: column;
  position: absolute;
  top: -14px;
  z-index: 100;
  border-radius: 2px;
  right: 214px;
  font-size: 14px;
  box-shadow: 2px 2px 4px grey;
  background: white;
  cursor: pointer;
}

.filter-menu .item-type {
}
.filter-menu .item-filter {
  height: 50%;
  display: flex;
  align-items: center;
}

.filter-menu .menu-child {
  width: 150px;
  height: 400px;
  display: flex;
  flex-direction: column;
  box-shadow: 2px 2px 4px grey;
}
.filter-menu .menu-child .item {
  height: 40px;
  display: flex;
  align-items: center;
  background: white;
  padding: 10px 5px;
  text-align: center;
}

.filter-menu .menu-container {
  position: absolute;
  left: 102px;
  top: 15px;
}

.filter-menu .text {
  padding: 0px 5px;
}
.filter-menu .item:hover {
  background: #eaeaea;
}

.filter-menu .menu-department {
  top: 40px;
}
.filter-menu .menu-child {
  display: none;
}

.item-filter:hover .menu-child {
  display: block;
}
.menu-child:hover {
  display: block;
}
table {
  position: relative;
}

.loading-emty {
  font-size: 24px;
  font-style: italic;
  white-space: nowrap;
  position: absolute;
  left: calc((100% - 180px) / 2);
  margin-top: 21px;
}

.v-icon {
  background-image: url("../../../assets/icon/add.png") !important;
  background-size: 16px;
  background-repeat: no-repeat;
  background-position: center;
  height: 28px;
  width: 28px;
}
.selected-row {
  background: #8dd2f369 !important;
}
.p-button {
  cursor: pointer;
}
.paging-toolbar .leftchild {
  justify-content: space-between;
  width: 355px;
}
.table-summary .price-number {
  position: absolute;
  right: 72px;
}
.content-grid {
  margin: 16px 16px 16px 16px;
  height: calc(100% - 165px);
  /* height: 100%; */
  position: relative;
  margin-top: 0;
  box-sizing: border-box;
}
.content-nav {
  padding-top: 8px;
  padding-bottom: 8px;
  border-top: 1px solid #d6d6d6;
  border-bottom: 1px solid #d6d6d6;
  height: auto;
}
.header-title {
  height: 80px;
  padding: 20px 16px;
}
.header-title {
  padding: 14px 16px;
  display: flex;
  align-items: center;
  height: auto;
}
.v-application .title {
  font-size: 1.25rem !important;
  font-weight: 500;
  line-height: 2rem;
  letter-spacing: 0.0125em !important;
  font-family: "GoogleSans-Regular";
  font-weight: 700;
}
.content-nav {
  padding-top: 12px;
  padding-bottom: 12px;
  border-top: 1px solid #d6d6d6;
  border-bottom: 1px solid #d6d6d6;
  height: auto;
}
.table-summary {
  font-size: 14px;
  font-family: GoogleSans-Medium;
  display: flex;
  /* position: relative; */
  width: 100%;
  position: absolute;
  bottom: 0px;
  /* align-items: center; */
  /* margin: 18px 16px 0 16px; */
  height: 47px;
  padding: 18px 16px;
  box-sizing: border-box;
}
.content-grid {
  margin-left: 0;
}

.btn {
  height: 34px;
  box-sizing: border-box;
  padding: 0 28px !important;
}
.btn.btn-close,
.btn.btn-help {
  border: none;
}
.btn {
  background-color: white;
  border: #e9e9e9 1px solid;
  font-size: 14px;
  padding: 0px 14px;
  border-radius: 4px;
  box-sizing: border-box;
  display: flex;
  align-items: center;
  height: 38px;
}
.icon-cancel {
  background-image: url(/img/cancel.cbb32219.svg);
  background-repeat: no-repeat;
  background-size: 18px 18px;
  background-position: center;
  width: 30px;
  height: 30px;
}
.btn.btn-close[data-v-134d14cc],
.btn.btn-help[data-v-134d14cc] {
  border: none;
  position: absolute;
  right: 5px;
}
.button-panel {
  display: flex;
}
.btn.btn-cancel[data-v-134d14cc] {
  position: absolute;
  right: 134px;
}
.btn[data-v-134d14cc] {
  height: 34px;
  box-sizing: border-box;
  padding: 0 14px !important;
}
.btn {
  height: 34px;
  box-sizing: border-box;
  padding: 0 28px !important;
  top: 6px;
  height: 30px !important;
  padding: 0 10px !important;
  /* width: 99px; */
}
.btn.btn-save {
  position: absolute;
  right: 42px;
  background: #00abfe;
}
table tbody tr td {
  /* border-right: 1px solid #eaeaea; */
  border-right: none;
}
.content-nav .features-pane .features-pane-left .icon-search {
  position: absolute;
  left: 0;
}
.input-search {
  width: 200px;
  padding: 8px 46px 8px 42px;
  border: 1px solid #beccc9;
}
.table-summary {
  margin-left: 0;
  background: #e5e5e5;
}
.footer-grid {
  display: flex;
  align-items: center;
  padding-top: 11px;
  height: auto;
  position: absolute;
  bottom: 0;
  width: 100%;
}
.container-box.grid-box {
  position: relative;
}
.features-pane-right #add-asset {
  right: 0 !important;
}
.content-grid[data-v-654548ea] {
  /* margin: 16px 16px 16px 16px; */
  margin: 0;
  height: calc(100% - 114px);
  position: relative;
  overflow: auto;
  box-sizing: border-box;
}
[data-v-654548ea]::-webkit-scrollbar {
  display: block;
  position: relative;
}
.btn.btn-confirm-delete1 {
  margin: auto;
  text-align: center;
  /* padding-left: 18px; */
}
.btn.btn-save {
  padding: 0 25px !important;
}
.btn.btn-cancel[data-v-654548ea] {
  box-sizing: border-box;
  padding: 0 25px !important;
}
.btn.btn-save[data-v-654548ea] {
  padding: 0 25px !important;
  margin-right: 11px;
}
.btn.btn-cancel[data-v-654548ea] {
  position: absolute;
  right: 139px;
}
.sumary {
  display: flex;
  height: 35px;
  align-items: center;
  /* background: red; */
  /* padding-top: 0px; */
  bottom: 0px;
  position: absolute;
  padding-left: 16px;
  width: 100%;
  border-top: 1px solid #d5d5d5;
}
.content {
  background: #f5f5f5;
}
th,
td {
  padding: 0px 10px 0px 16px;
}
.icon-add[data-v-654548ea] {
  height: 25px;
  width: 30px;
  background: url(/img/qlts-icon.d656886f.svg) no-repeat -143px -416px;
  background-repeat: no-repeat;
  background-size: 376px 1198px;
}
.modal-content .header {
  border-bottom: 1px solid #e4e4e4;
  background: white;
  border-radius: 4px 4px 0 0;
}
.modal-content .footer {
  border-radius: 0 0 4px 4px;
}
.modal-content {
  border-radius: 4px;
}
.modal-content .content[data-v-654548ea] {
  padding: 16px;
  padding-top: 4px;
}

.modal-container.container-below[data-v-654548ea] {
  height: 58%;
  padding-top: 8px;
}
.panel-header[data-v-654548ea][data-v-654548ea] {
  /* padding-bottom: 20px; */
  padding: 16px;
  padding-bottom: 8px;
  padding-top: 8px;
}
.features-pane-right #add-asset[data-v-654548ea][data-v-654548ea] {
  width: auto;
  height: 34px !important;
}
.content-grid[data-v-654548ea][data-v-654548ea] {
  /* margin: 16px 16px 16px 16px; */
  margin: 0;
  height: calc(100% - 87px);
  position: relative;
  overflow: auto;
  box-sizing: border-box;
}
.btn.btn-save[data-v-654548ea] {
  position: absolute;
  right: 19px;
  background: #00abfe;
}
.btn.btn-cancel[data-v-654548ea][data-v-654548ea] {
  position: absolute;
  right: 131px;
}
.icon-cancel {
  top: 7px;
}
.features-pane-right[data-v-654548ea][data-v-654548ea] {
  position: absolute;
  right: 16px;
}

.icon-refresh-time1 {
  height: 26px !important;
  width: 34px !important;
  background: url(/img/qlts-icon.d656886f.svg) no-repeat -396px -120px;
  background-repeat: no-repeat;
  border-radius: 4px;
  background-size: 440px 1207px;
}
.btn.btn-confirm-delete1:hover {
  border: 1px solid red;
}
.btn.btn-confirm-delete1 {
  border: none;
}
.btn:hover {
  cursor: pointer;
  background-color: rgba(214, 214, 214, 0.829);
}
.btn-save:hover {
  background-color: #29b8ff !important;
}
.inputMoney {
  border: 1px solid #ffffff;
  background-clip: padding-box;
}

table tbody tr td {
  max-width: 123px;
}
table tbody tr td:nth-child(7) {
  max-width: 130px;
}
table tbody tr td:nth-child(5) {
  padding: 0;
  width: 130px;
}
.btn {
  height: 34px !important;
}

.below-label,
.top-label {
  font-size: 16px;
}

.container-box {
  box-shadow: none;
}

.btn.btn-save {
  position: absolute;
  right: 19px;
  background: #178aa9;
}
.modal-content {
  top: calc((100% - 732px) / 2) !important;
  left: calc((100% - 980px) / 2) !important;
  height: 732px !important;
  width: 980px;
}
.emty-text {
  position: absolute;
  top: 93px;
  left: 37%;
}
.input-field label {
  font-family: "GoogleSans-Medium" !important;
}
.input-sm .datetime {
  background-position: 183px center;
}

// xác nhận xóa
.btn.btn-confirm-delete {
  background-color: #ff4646;
  color: white;
}
.btn.btn-confirm-delete:hover {
  background-color: #ff7777;
}

.modal1 {
  .modal1-content {
    position: relative;
    top: calc(50% - 100px);
    left: calc(50% - 175px);
    width: 350px;
    height: 200px;
    background-color: white;

    .modal1-header {
      height: 60px;
      padding: 0 16px;
      line-height: 60px;
      font-size: 18px;
      font-weight: 500;
    }

    .modal1-content {
      height: calc(100% - 110px);
      padding: 16px;
    }
    .modal1-footer {
      height: 50px;
      display: flex;
      align-items: center;
      justify-content: flex-end;
      background-color: #f5f5f5;
      padding: 0 10px;
      //TODO set auto focus vao nut xoa
      .btn {
        margin: 0 6px;
        height: 34px;
      }
    }
  }
}

.modal1 .modal1-content {
  border-radius: 4px;
}
.modal1 .modal1-content .modal1-header {
  border-bottom: 1px solid #e6e6e6;
}

.modal1 .modal1-content .modal1-footer {
  border-radius: 4px;
}
.modal1 .modal1-content {
  height: auto;
}
.modal1 .modal1-content .modal1-header {
  height: 50px;
}
.icon-cancel {
  background-image: url(/img/cancel.cbb32219.svg);
  background-repeat: no-repeat;
  background-size: 14px 18px;
  background-position: center;
  /* width: 20px; */
  /* height: 20px; */
  position: absolute;
  right: 12px;
  top: 5px;
  cursor: pointer;
}

.modal1 .modal1-content .modal1-header {
  display: flex;
  align-items: center;
}
.icon-cancel.btn.btn-close {
  padding: 0 !important;
}
.div-container.modal1 {
  position: relative;
  top: calc(50vh - 153px);
  z-index: 2;
}
.modal .modal-background {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: black;
  opacity: 0.8;
}
.btn.btn-save {
  position: absolute;
  right: 19px;
  background: #1ba3c9;
}
.btn-save-fake:hover {
  background: #29b8ff !important;
}
.btn-save-fake {
  color: white;
}
.icon-help {
  background-repeat: no-repeat;
  background-size: 18px 18px;
  background-position: center;
  width: 30px;
  margin-right: 32px;
  margin-top: 5px;
  height: 30px;
}
.input-sm .datetime {
  background-position: 184px center;
}
.inputMoney {
  border: 1px solid #000000;
  height: 30px;
  background-clip: padding-box;
}
table tr td:nth-child(7) {
  width: 130px;
}

.alert {
  border-color: red !important;
}
.modal-content{
    top: calc((100% - 620px) / 2) !important;
    left: calc((100% - 980px) / 2) !important;
    height: 620px !important;
    width: 980px;
}

.inputMoney {
    border: 1px solid #d2d2d2;
    height: 30px;
    border-radius: 3px;
    background-clip: padding-box;
}
.content {
    background: #F5F6FA;
}
.footer {
    width: 100%;
    height: 50px;
    background-color: #F5F5F5;
    display: flex;
    align-items: center;
    position: relative;
    padding: 0 16px;
}
.inputMoney:focus{
  border: 1px solid black;
}

.modal-content {
    top: calc((100% - 88%) / 2) !important;
    left: calc((100% - 75%px) / 2) !important;
    height: 88% !important;
    width: 75%;
}
.below-label {
    display: flex;
    align-items: center;
}
table tbody tr td {
    border-bottom: 1px solid #f1f1f1;
}

.modal-container.container-below {
    height: 69%!important;
    padding-top: 8px;
}
.modal-container{
    width: 100%;
    height: 31%;
}
table th:nth-child(1)
{
  width: 4%;
}

table th:nth-child(2), th:nth-child(3)
{
  width: 10%;
}
th:nth-child(4){
  width: 20%;
}
th:nth-child(8)
{
  width: 8%;
}
table th:nth-child(5), th:nth-child(6)
{
  width: 15%;
}
</style>