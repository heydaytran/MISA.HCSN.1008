<template>
  <div v-show="isChoose" class="div-container content" id="content">
     <div class="modal-background1"></div>
    <div class="div-container">
      <div class="header-title">
        <div class="title">Chọn tài sản ghi tăng</div>
        <div
          class="icon-cancel btn btn-close"
          title="Đóng"
          @click="hide()"
        ></div>
      </div>
      <div class="content-nav">
        <v-alert
          id="success-dialog"
          v-show="showSuccess"
          color="green"
          elevation="30"
          type="success"
        >
          {{ alerMsg }}</v-alert
        >
        <v-alert v-if="showWarning" id="required-choose" type="warning">{{
          alerMsg
        }}</v-alert>

        <!-- <v-alert v-if="showError" id="required-choose" type="warning">{{
          alerMsg
        }}</v-alert> -->

        <div class="features-pane">
          <div class="features-pane-left">
            <input
              id="assetSearchBox"
              class="input-search"
              type="text"
              placeholder="Tìm kiếm theo tên, mã tài sản. "
              v-model="inputSearch"
              @change="
                getAsset(
                  'filter',
                  comboxFilter.idDepartment,
                  comboxFilter.idType
                )
              "
            />
            <div class="icon-search" title="Tìm kiếm"></div>
          </div>
        </div>

        <div class="clear-float"></div>
      </div>

      <div id="loadBar"></div>
      <div class="content-grid grid">
        <table class="table-asset" id="tableAsset">
          <colgroup>
            <col width="0px" />
            <col width="0px" />
            <col min-width="0px" />
            <col min-width="300px" />
            <col min-width="0px" />
            <col width="0px" />
            <col width="0px" />
          </colgroup>
          <thead>
            <tr>
              <th style="text-align: center">
                <input
                  @click="checkAll()"
                  class="checkboxAll"
                  type="checkbox"
                />
              </th>
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
              <th title="Hao mòn, khấu hao lũy kế" class="hover-pointer" style="text-align: right">
                HM/KM lũy kế
              </th>
              <th class="hover-pointer" style="text-align: right">
                Giá trị còn lại
              </th>
            </tr>
          </thead>

          <tbody>
            <tr
              v-for="(asset, index) in listAsset"
              :key="asset.assetId"
              v-bind:class="selectedRow(asset.assetId) ? 'selected-row' : ''"
              @click="
                selectRow(index, $event, asset.assetId, 'rowClick')
              "
            >
              <td style="text-align: center">
                <input
                  class="checkbox"
                   @click.stop="
                    selectRow(index, $event, asset.assetId , '')
                  "
                  type="checkbox"
                />
              </td>
              <td class="no-border-left">{{ index + 1 }}</td>
              <td>{{ asset.assetCode }}</td>
              <td>{{ asset.assetName }}</td>
              <td>{{ asset.departmentName }}</td>
              <td style="text-align: right">
                {{ asset.originalPrice | formatMoney(asset.originalPrice) }}
              </td>
              <td style="text-align: right">
                {{ asset.wearValue | formatMoney(asset.originalPrice) }}
              </td>

              <td class="no-border-right" style="text-align: right">
                {{
                  (asset.originalPrice - asset.wearValue)
                    | formatMoney(asset.originalPrice - asset.wearValue)
                }}
              </td>
            </tr>
          </tbody>
          <BaseLoading ref="BaseLoading_ref" />

          <div v-show="getEmty" class="loading-emty">Không có dữ liệu</div>
        </table>
      </div>

      <div class="table-summary">
        <div class="summary">
         

          <div class="paging-toolbar">
            <div class="leftchild">
              <div
                title="Trang đầu"
                class="p-button first-page"
                @click="firstPage()"
              ></div>
              <div
                title="Trang trước"
                class="p-button prev-page"
                @click="backPage()"
              ></div>
              <div>Trang</div>
              <input
                type="number"
                class="text-pagebumber"
                v-model="paging.pageNumber"
                @change="reloadPage()"
              />
              <div>Trên {{ paging.amountPage }}</div>

              <div
                title="Trang sau"
                class="p-button next-page"
                @click="nextPage()"
              ></div>
              <div
                title="Trang cuối"
                class="p-button last-page"
                @click="lastPage()"
              ></div>
              <div
                title="Tải lại"
                class="p-button refresh"
                @click="getAsset()"
              ></div>
              <select
                title="Số bản ghi trên 1 trang"
                name=""
                id=""
                class="select-quantitypage"
                v-model="paging.recordNumber"
                @change="getAsset('filter')"
              >
                <option value="25">25</option>
                <option value="50">50</option>
                <option value="100">100</option>
              </select>
            </div>
          </div>

          <div class="button-panel">
            <div class="btn btn-cancel" tabindex="0" @click="hide()">
              Hủy bỏ
            </div>
            <div class="btn btn-save" tabindex="0" @click="sendAssetList()">
              Đồng ý
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseLoading from "../../common/BaseLoading.vue";
import axios from "axios";

export default {
  components: {
    BaseLoading,
  },
  props: {
    isFilterMenu: Boolean,
    listAssetView: {
      type: Array,
      default: () => [],
    },
  },
  data() {
    return {
      listAsset: [],
      asset: {
        assetId: null,
        assetCode: null,
        assetName: null,
        departmentId: null,
        assetTypeId: null,
        timeUse: null,
        wearRate: null,
        originalPrice: null,
        wearValue: null,
      },
      listDepartment: [],
      listAssetType: [],
      formMode: "",
      alerMsg: "",
      assetIdUpdate: null,
      listSelectRow: [],
      listAssetId: [],
      inputSearch: "",
      showSuccess: false,
      isError: false,
      getSuccess: true,
      getEmty: false,
      totalPrice: 0,
      amountAsset: 0,
      showWarning: false,
      paging: {
        amountPage: 1,
        pageNumber: 1,
        recordNumber: 50,
        totalRecord: 0,
        startRecord: 0,
        endRecord: 0,
      },
      showMenuFilter: true,
      showMenuType: false,
      showMenuDepartment: true,
      comboxFilter: {
        idDepartment: null,
        idType: null,
        allAsset: "tất cả",
      },
      isChoose: false,
    };
  },
  methods: {
    
    // todo check tất cả các check box
    checkAll() {
      this.listSelectRow = []
      var res = this;
      if (document.getElementsByClassName("checkboxAll")[0].checked == true) {
        this.listAssetId.forEach((id) => {
          res.listSelectRow.push(id);
        });
        document.getElementsByClassName("checkbox").forEach((element) => {
          element.checked = true;
        });
      } else {
        res.listSelectRow = [];
        document.getElementsByClassName("checkbox").forEach((element) => {
          element.checked = false;
        });
      }
    },
    // todo hiển thị dialog
    show() {
      this.inputSearch = ''
      this.listSelectRow = [];
      this.isChoose = true;
      document.getElementsByClassName("checkboxAll").forEach((element) => {
        element.checked = false;
      });

      this.getAsset();
    },

    // todo ẩn dialog
    hide() {
      this.isChoose = false;
    },

    // todo hàm xóa dữ liệu 1 phần từ
    arrayRemove(arr, value) {
      return arr.filter(function (ele) {
        return ele != value;
      });
    },
    /**
     * Gửi request GET tới API
     * Author: TVThinh (12/5/2021)
     */
    async getAsset(text) {

      // xử lý filter trên thanh combobox
      if (text == "filter") {
        this.paging.pageNumber = 1;
        
      }
      else if(text == 'pageChange')
      {
        console.log('hello');
      }
      else{
        this.listSelectRow = [];
      }
       //debugger; // eslint-disable-line no-debugger

      var res = this;
      
      res.$refs.BaseLoading_ref.show();
      this.getEmty = false;
      this.amountAsset = 0;
      this.totalPrice = 0;

      // if (document.getElementById("ctxMenu"))
      //   // nếu context menu vẫn hiện
      //   document.getElementById("ctxMenu").style.display = "none";

      await axios
        .get(
          "https://localhost:44382/api/v1/Assets/Filter/?input=" +
            res.inputSearch +
            "&recordAmount=" +
            this.paging.recordNumber +
            "&pageNumber=" +
            this.paging.pageNumber 
        )
        .then((response) => {
          res.listAsset = response.data.data;

          // todo xóa các tài sản đã chọn
         if(res.listAssetView!=null)
         {
            for (var j = 0; j < res.listAssetView.length; j++) {
            res.listAsset = res.listAsset.filter(function (ele) {
              return ele.assetId != res.listAssetView[j].assetId;
            });
          }
         }

        // nếu tài sản rỗng thì hiển thị dữ liệu rỗng
          if (res.listAsset.length == 0) {
            res.getEmty = true;
          }
          else {
            res.getEmty = false;
          }
          //debugger; // eslint-disable-line no-debugger

        // tắt form loading
          res.$refs.BaseLoading_ref.hide();
          res.listAssetId = [];
          res.paging.amountPage = response.data.totalPage;

          res.listAsset.forEach((element) => {
            // duyệt qua tất cả các bản ghi
            res.listAssetId.push(element.assetId); // push tất cả id tài sản vào mảng
            res.amountAsset++; // đếm tổng số bản ghi
           
          });
        })
        .catch((error) => {
          console.log(error);
          setTimeout(() => {
            res.$refs.BaseLoading_ref.hide(); // tắt dialog loading
            res.getEmty = true; // b
          }, 4000);
        });

      //   //select bản ghi đầu tiên
      //   setTimeout(() => {
      //     this.listSelectRow.push(this.listAssetId[0]);
      //   }, 0);
    },

    /// todo lấy ra các phòng ban
    async getDepartment() {
      var res = this;
      await axios
        .get("https://localhost:44382/api/v1/Departments")
        .then((response) => {
          res.listDepartment = response.data.data;
        })
        .catch((error) => {
          this.errorMessage = error.message;
          console.error("GET Department Failed: ", error.message);
        });
    },

    ///  todo lấy ra các loại tài sản
    async getAssetType() {
      var res = this;
      await axios
        .get("https://localhost:44382/api/v1/AssetTypes")
        .then((response) => {
          res.listAssetType = response.data.data;
        })
        .catch((error) => {
          this.errorMessage = error.message;
          console.error("GET AssetType Failed: ", error.message);
        });
    },

    // todo tải lại dữ liệu
    reload(value) {
      if (value == true) {
        this.getAsset("");
        this.showSuccess = true;
        setTimeout(() => {
          this.showSuccess = false;
        }, 3000);
      }
    },

   //  select hàng, nếu hàng đã được select thì xóa khỏi mẩng listSelectRow, và ngược lại
    selectRow(ind, e, id, text) {
      document.getElementsByClassName('checkboxAll')[0].checked = false
      if (e.shiftKey) {
        var idFirst = this.listSelectRow[0];
        this.listSelectRow = [];
        this.listSelectRow.push(idFirst);

        // vị trí đầu tiên trong mảng listSelectRow
        var idStart = this.listSelectRow[0];
        var indexStart = this.listAssetId.indexOf(idStart);

        var indexEnd = this.listAssetId.indexOf(id);
        if (indexStart > indexEnd) {
          indexStart--;
          var tem = indexStart;
          indexStart = indexEnd;
          indexEnd = tem;
        } else {
          indexStart++;
        }
        for (var i = indexStart; i <= indexEnd; i++) {
          this.listSelectRow.push(this.listAssetId[i]);
        }
      } else if (e.ctrlKey) {
        var index = this.listSelectRow.indexOf(id);
        if (index > -1) {
          this.listSelectRow.splice(index, 1);
        } else {
          this.listSelectRow.push(id);
        }
      } else if (e.ctrlKey == false && e.shiftKey == false) {
        if (text != "rowClick") {
          var index1 = this.listSelectRow.indexOf(id);
          if (index1 > -1) {
            this.listSelectRow.splice(index, 1);
          } else {
            this.listSelectRow.push(id);
           
           
          }
        } else {
          document.getElementsByClassName("checkbox").forEach((el) => {
            el.checked = false;
          });
         
         
          this.listSelectRow = [];
          this.listSelectRow.push(id);
        }
       
      }
    },

    // kiểm tra hàng đã được select hay chưa
    selectedRow(id) {
      if (this.listSelectRow.indexOf(id) > -1) return true;
      else return false;


    },

    // todo xử lý sự kiện mũi tên lên xuống để select row
    processkey() {
      var res = this;
      document.addEventListener("keydown", function (e) {
        var len1 = res.listSelectRow.length; // số phần tử của mảng listSelectRow
        var len2 = res.listAssetId.length; //số phần tử của mảng listAssetId
        switch (e.keyCode) {
          case 38:
            {
              //up arrow
              if (
                len1 == 0 ||
                res.listAssetId.indexOf(res.listSelectRow[0]) == 0
              ) {
                res.listSelectRow = [];
                res.listSelectRow.push(res.listAssetId[len2 - 1]);
              } else if (len1 > 0) {
                //res.listSelectRow = [];
                var indexIdFirst = res.listAssetId.indexOf(
                  res.listSelectRow[0]
                );
                res.listSelectRow = [];
                res.listSelectRow.push(res.listAssetId[indexIdFirst - 1]);
              }
            }
            break;
          case 40:
            {
              // //down arrow
              if (
                len1 == 0 ||
                res.listAssetId.indexOf(res.listSelectRow[len1 - 1]) == len2 - 1
              ) {
                res.listSelectRow = [];
                res.listSelectRow.push(res.listAssetId[0]);
              } else if (len1 > 0) {
                //res.listSelectRow = [];
                var indexIdLast = res.listAssetId.indexOf(
                  res.listSelectRow[len1 - 1]
                );
                res.listSelectRow = [];
                res.listSelectRow.push(res.listAssetId[indexIdLast + 1]);
              }
            }
            break;
        
          default: {
            return true;
          }
        }
      });
    },

    // todo chuyển đến trang sau
    nextPage() {
      if (this.paging.pageNumber < this.paging.amountPage) {
        this.paging.pageNumber++;
        this.getAsset('pageChange');
      }
    },
    // todo chuyển đến trang trước
    backPage() {
      if (this.paging.pageNumber > 1) {
        this.paging.pageNumber--;
        this.getAsset('pageChange');
      }
    },

    // todo về trang cuối
    lastPage() {
      this.paging.pageNumber = this.paging.amountPage;
      this.getAsset('pageChange');
    },

    // todo về trang đầu
    firstPage() {
      this.paging.pageNumber = 1;
      this.getAsset('pageChange');
    },

    // todo load lại dữ liệu
    reloadPage() {
      if (
        parseInt(this.paging.pageNumber) <= this.paging.amountPage &&
        parseInt(this.paging.pageNumber) > 0
      ) {
        this.getAsset();
      } else alert("Trang không hợp lệ");
    },

    // todo gửi danh sách tài sản lên
    sendAssetList() {
      if( this.listSelectRow.length == 0)
      {
          alert('Vui lòng chọn tài sản')

          return
      }
      else{
      this.hide();
      this.$emit("listAssetView", this.listSelectRow);
      }

    },
  },
  filters: {
    // todo định dạng kiểu tiền tệ cho nguyên giá
    formatMoney: function (money) {
      if (money != null)
        var num = money.toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
      else return "0";
      return num;
    },
  },
  updated() {
    document.getElementsByClassName("checkbox").forEach(ele => ele.checked = false)
       document.getElementsByClassName('selected-row').forEach(ele => {
           ele.firstElementChild.firstElementChild.checked = true
        })
  },
  created() {
    //this.getAsset();
    this.processkey();
  },
  mounted() {
    this.getDepartment();
    this.getAssetType();
    this.getAsset();
  },
};
</script>


<style lang='scss' scoped>
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
  overflow: auto;
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
  width: 275px;
  animation-duration: 3s;
  white-space: nowrap;
  padding: 16px 0px;
  bottom: 0px;
}

@keyframes alert {
  0% {
    width: 275px;
    padding: 16px;
  }
  50% {
    width: 275px;
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
  overflow: auto;
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
  background: #8dd2f369 !important
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
  overflow: auto;
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
  position: absolute !important;
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
div#content {
    background: white;
}
table thead tr th{
  background: #e5e5e5;
}
.div-container {
    border-radius: 4px;
}
.btn:hover {
    cursor: pointer;
    background-color: rgba(214, 214, 214, 0.829);
}
.btn-save:hover{
   background-color: #29b8ff!important;
}
.table-summary {
    margin-left: 0;
    background: #f5f5f5;
}
.content-grid{
    margin: 0;
}

.content-nav .features-pane input {
    border-radius: 4px;
}
table tr td:last-child, table th:last-child{
  padding-right:34px ;
}
table tr td:nth-child(6), table th:nth-child(6){
  padding-right:34px ;
}
table tr td:nth-child(7), table th:nth-child(7){
  padding-right:34px ;
}
.btn {
    height: 34px!important;
}
.table-summary {
    
    height: 50px;
 
}
.paging-toolbar{
 margin: 0;
}
.btn.btn-save{
    position: absolute;
    right: 38px;
    background: #00abfe;
}
.btn.btn-close, .btn.btn-help {
    border: none;
    position: absolute;
    right: 31px!important;
    top: 14px!important;
}

.modal-background1 {
    position: fixed;
    top: 0;
    left: 0;
    background-color: black;
    opacity: 0.8;
    right: 0;
    bottom: 0;
    z-index: 1;
}
.content .div-container[data-v-134d14cc] {
    height: 100%;
    z-index: 6;
    background: white;
}
.btn.btn-save {
    position: absolute;
    right: 38px;
    background: #1BA3C9;
}
.modal-background1 {
  opacity: 0.6;
}
.content-nav {
  padding-left: 6px;
}
.table-summary {
  padding: 18px 20px;
}
table th:first-child{
  padding-left: 20px;
}
table td:first-child{
  padding-left: 20px;
}
.loading-emty {
  font-size: 20px;
}
.table-summary{
    padding: 18px 20px;
    border-radius: 5px;
}
div#content {
    z-index: 1;
    height: 73%;
    width: 65%;
    position: absolute;
    top: calc((100% - 73%) / 2);
    left: calc((100% - 65%) / 2);
}
table th:nth-child(1), th:nth-child(2)
{
  width: 4%;
}

table th:nth-child(3), th:nth-child(4)
{
  width: 10%;
}
th:nth-child(5){
  width: 20%;
}
</style> 