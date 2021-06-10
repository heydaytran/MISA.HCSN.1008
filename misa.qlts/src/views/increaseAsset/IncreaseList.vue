<template>
  <div class="div-container content" id="content">
    <div class="div-container">
      <div class="panel-feature-top">
        <div class="panel-feature">
          <div class="feature-left">
            <div class="text-title">
              <div class="text">Ghi tăng tài sản</div>
            </div>
            <div
              title="Tải lại trang"
              class="button-refresh"
              @click="getAssetIncrease()"
            >
              <div class="hover-pointer icon"></div>
            </div>
          </div>
          <div class="feature-right">
            <div
              class="btn-add-asset btn features-pane-item"
              @click="showDialog('insert', 0)"
            >
              Thêm
            </div>
            <div class="hover-pointer button-option-view" title="Chế độ xem">
              <div
                title="Chế độ xem"
                class="hover-pointer icon-view"
                v-bind:class="{
                  'icon-view-all': viewAll,
                  'icon-view-detail': !viewAll,
                }"
              ></div>
              <div title="Chế độ xem" class="hover-pointer icon-arrow"></div>
              <div class="menu-option-view">
                <div
                  title="Xem tổng quát"
                  class="item-option option-full"
                  @click="changeVisual('all')"
                >
                  <div v-if="viewAll" class="hover-pointer icon-chosen"></div>
                  <div class="hover-pointer icon-view-all"></div>
                </div>
                <div
                  title="Xem chi tiết"
                  class="item-option option-detail"
                  @click="changeVisual('detail')"
                >
                  <div v-if="!viewAll" class="hover-pointer icon-chosen"></div>
                  <div class="hover-pointer icon-view-detail"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
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
              placeholder="Tìm kiếm. "
              v-model="inputSearch"
              @change="getAssetIncrease('filter')"
            />
            <div class="icon-search"></div>
          </div>

          <div class="features-pane-right">
            <div class="button-print">
              <div class="icon"></div>
            </div>

            <div
              id="preventLeftClick"
              title="Xóa nhiều chứng từ"
              class="btn icon-trash features-pane-item"
              @click="showDeleteDialog()"
            ></div>
            <div
              class="btn icon-print features-pane-item"
              title="In chứng từ"
            ></div>
            <div
              class="btn icon-more features-pane-item"
              title="Tùy chọn"
            ></div>
          </div>
        </div>

        <div class="clear-float"></div>
      </div>

      <div id="loadBar"></div>
      <div class="content-grid-all content-grid grid">
        <table class="table-increase table-asset" id="tableAsset">
          <colgroup>
            <col width="0px" />
            <col width="0px" />
            <col min-width="100px" />
            <col min-width="0px" />
            <col min-width="0px" />
            <col width="0px" />
            <col width="0px" />
          </colgroup>
          <thead>
            <tr>
              <th style="text-align:left">
                <div class="checkbox-icon" style="width:16px">
                  <input
                    class=" checkboxAll1"
                    @click="checkAll()"
                    type="checkbox"
                  />
                </div>
              </th>
              <th style="text-align: left">STT</th>

              <th
                sortProp="code"
                sortOrder="asc"
                id="columnAssetCode"
                class="hover-pointer"
                style="text-align: left; width:10%"
              >
                SỐ CHỨNG TỪ
              </th>
              <th
                sortProp="code"
                sortOrder="asc"
                id="columnAssetCode"
                class="hover-pointer"
                style="text-align: center;width: 18%px;"
              >
                NGÀY CHỨNG TỪ
              </th>
              <th
                sortProp="code"
                sortOrder="asc"
                id="columnAssetCode"
                class="hover-pointer"
                style="text-align: center;width: 18%;"
              >
                NGÀY GHI TĂNG
              </th>
              <th
                sortProp="name"
                sortOrder="asc"
                id="columnAssetName"
                class="hover-pointer"
                style="text-align: left; width: 24%;"
              >
                NỘI DUNG
              </th>
              <th
                sortProp="department"
                sortOrder="asc"
                id="columnDepartment"
                class="hover-pointer"
                style="text-align: right ; width: 20%;"
              >
                TỔNG NGUYÊN GIÁ
              </th>

              <th style="text-align: left; width: 10%;" ></th>
            </tr>
          </thead>

          <tbody>
            <!-- <tr
              v-for="(asset, index) in listAssetIncrease"
              :key="asset.assetId"
              v-bind:class="selectedRow(asset.assetId) ? 'selected-row' : ''"
              @click="selectRow(asset.assetId, $event)"
              @click.right="showContexMenu(asset.assetId, $event)"
            >
              <td class="no-border-left">{{ index + 1 }}</td>
              <td>{{ asset.assetCode }}</td>
              <td>{{ asset.assetName }}</td>
              <td>{{ asset.assetTypeName }}</td>
              <td>{{ asset.departmentName }}</td>

              <td class="no-border-right">
                <div class="features-box">
                  <div
                    :id="'tableRow' + index + '_edit'"
                    class="table-icon icon-edit-pen"
                    @click="showDialog('update', asset.assetId)"
                    title="Sửa"
                  ></div>
                  <div
                    id="preventLeftClick"
                    class="table-icon icon-trash-table"
                    @click="showDeleteDialog('inRow')"
                    title="Xóa"
                  ></div>
                  <div
                    class="table-icon icon-refresh-time"
                    title="Chức năng chưa phát triển"
                  ></div>
                </div>
              </td>
            </tr> -->

            <tr
              v-for="(item, index) in listAssetIncrease"
              v-bind:class="{
                'selected-row': selectedRow(item.assetIncreaseId),
                '': !selectedRow(item.assetIncreaseId),
              }"
              @click="
                selectRow(index, $event, item.assetIncreaseId, 'rowClick')
              "
              @dblclick.stop="showDialog('update', item.assetIncreaseId)"
              :key="item.assetIncreasesId"
            >
              <td>
                <input
                  @click.stop="
                    selectRow(index, $event, item.assetIncreaseId, '')
                  "
                  class="checkbox1"
                  type="checkbox"
                />
              </td>
              <td>{{ index + 1 }}</td>
              <td @click.stop="showDialog('update', item.assetIncreaseId)">{{ item.exhibitCode }}</td>
              <td style="text-align:center">{{ item.exhibitDate | formatDate(item.exhibitDate) }}</td>
              <td style="text-align:center">{{ item.increaseDate | formatDate(item.increaseDate) }}</td>
              <td>{{ item.note }}</td>
              <td style="text-align: right">
                {{ totalPriceEach[index] | formatMoney(totalPriceEach[index]) }}
              </td>
              <td class="no-border-right">
                <div class="features-box">
                  <div class="table-icon icon-edit-pen1" title="In "></div>
                  <div
                    class="table-icon icon-trash-table1"
                    @click.stop="showDialog('update', item.assetIncreaseId)"
                    title="Sửa"
                  ></div>
                  <div
                    class="table-icon icon-refresh-time1"
                    @click.stop="
                      showDeleteDialog('inRow', item.assetIncreaseId, $event)
                    "
                    title="Xóa"
                  ></div>
                </div>
              </td>
            </tr>
          </tbody>
          <BaseLoading ref="BaseLoading_ref" />

          <div v-show="getEmty" class="loading-emty">Không có dữ liệu</div>
        </table>
      </div>

      <div class="grid-detail-container">
        <div class="header-table-detail text-title">
          <div @mousedown="resizeDetail($event)" class="resize-icon">
            <div class="icon-top"></div>
            <div class="icon-center"></div>
            <div class="icon-bottom"></div>
          </div>
        </div>
        <div class="title">THONG TIN CHI TIET</div>
        <div class="content-grid-detail content-grid grid">
          <table class="table-detail table-asset" id="tableAsset">
            <colgroup>
              <col width="20px" />
              <col width="200px" />
              <col min-width="200px" />
              <col min-width="300px" />
              <col min-width="300px" />
              <col min-width="300px" />
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
                  Mã tài sản
                </th>
                <th
                  sortProp="code"
                  sortOrder="asc"
                  id="columnAssetCode"
                  class="hover-pointer"
                  style="text-align: left"
                >
                  Bộ phận sử dụng
                </th>
                <th
                  sortProp="code"
                  sortOrder="asc"
                  id="columnAssetCode"
                  class="hover-pointer"
                  style="text-align: right"
                >
                  Nguyên giá
                </th>
                <th
                  sortProp="name"
                  sortOrder="asc"
                  id="columnAssetName"
                  class="hover-pointer"
                  style="text-align: right"
                >
                  HM/KH lũy kế
                </th>
                <th
                  sortProp="department"
                  sortOrder="asc"
                  id="columnDepartment"
                  class="hover-pointer"
                  style="text-align: right"
                >
                  Giá trị còn lại
                </th>
              </tr>
            </thead>

            <tbody>
              <tr
                v-for="(item, index) in increaseDetails"
                v-show="listAssetIncrease[indexDetail] !== undefined"
                v-bind:class="
                  selectedRow(item.assetIncreaseId) ? 'selected-row' : ''
                "
                :key="item.assetIncreasesId"
              >
                <td>{{ index + 1 }}</td>
                <td :title="item.assetCode">{{ item.assetCode }}</td>
                <td :title="item.departmentName">{{ item.departmentName }}</td>
                <td
                  :title="item.originalPrice | formatMoney(item.originalPrice)"
                  style="text-align: right"
                >
                  {{ item.originalPrice | formatMoney(item.originalPrice) }}
                </td>
                <td
                  style="text-align: right"
                  :title="item.wearValue | formatMoney(item.wearValue)"
                >
                  {{ item.wearValue | formatMoney(item.wearValue) }}
                </td>
                <td
                  style="text-align: right"
                  :title="item.resValue | formatMoney(item.resValue)"
                >
                  {{ item.resValue | formatMoney(item.resValue) }}
                </td>
              </tr>
            </tbody>
            <BaseLoading ref="BaseLoading_ref" />

            <div v-show="getEmty" class="loading-emty">Không có dữ liệu</div>
          </table>
        </div>
      </div>

      <div id="assetPopup"></div>
    </div>

    <ModalCreate
      ref="ModalCreateAsset_ref"
      :formMode="formMode"
      :assetIdUpdate="assetIdUpdate"
      @reload="reload"
      @msgAlert="msgAlert"
      @statusForm="statusForm"
    />
    <ModaDelete
      @reload="reload"
      @statusForm="statusForm"
      :listSelectRow="listSelectRow"
      ref="ModalDeleteAsset_ref"
      :formMode="formMode"
    />
  </div>
</template>

<script>
import ModalCreate from "../../components/modal/increaseAssetModal/ModalCreate.vue";
import ModaDelete from "../../components/modal/increaseAssetModal/ModalDelete.vue";
import BaseLoading from "../../components/common/BaseLoading.vue";
import axios from "axios";

export default {
  components: {
    ModaDelete,
    ModalCreate,
    BaseLoading,
  },
  props: {
    isFilterMenu: Boolean,
  },
  data() {
    return {
      listAssetIncrease: [],
      indexDetail: 0, // vị trí của bản ghi muốn xem detail
      formMode: "",
      alerMsg: "",
      assetIdUpdate: null,
      listSelectRow: [],
      listAssetIncreaseId: [],
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
      totalPriceEach: [], // tổng nguyên giá mỗi chứng từ ghi tăng
      viewAll: false,
      detailReady: false, // = true khi load đc hết các bản ghi
      resizing: false, // có cho phép resize k
      increaseDetails: [],
    };
  },
  methods: {
    //todo nhận trạng thái tắt form
    statusForm(value) {
      this.formMode = value;
    },
    // todo nhận các câu thông báo cho modal thông báo
    msgAlert(text, value) {
      this.showWarning = value;
      this.alerMsg = text;
      var res = this;

      setTimeout(() => {
        res.showWarning = false;
      }, 3000);
    },

    //todo hủy chọn các checkbox
    turnOffCheck() {
      document.getElementsByClassName("checkbox1").forEach((e) => {
        e.checked = false;
      });
    },
    // todo đổi tiền tệ kiểu string về số
    fomatMoneyToNumber(value) {
      if (value != "") {
        return parseFloat(value.split(".").join(""));
      }
      return 0;
    },
    /**
     * Gửi request GET tới API
     * Author: TVThinh (12/5/2021)
     */
    async getAssetIncrease(text) {
      if (text == "filter") this.paging.pageNumber = 1;
      else this.inputSearch = "";

      this.turnOffCheck();
      this.detailReady = false;
      var res = this;
      this.listSelectRow = [];

      res.$refs.BaseLoading_ref.show();
      this.getEmty = false;
      this.amountAsset = 0;
      this.totalPrice = 0;

      if (document.getElementById("ctxMenu"))
        // nếu context menu vẫn hiện
        document.getElementById("ctxMenu").style.display = "none";

      await axios
        .get(
          "https://localhost:44382/api/v1/AssetIncreases/Filter/?input=" +
            res.inputSearch +
            "&recordAmount=" +
            this.paging.recordNumber +
            "&pageNumber=" +
            this.paging.pageNumber
        )
        .then((response) => {
          res.listAssetIncrease = response.data.data;
          res.detailReady = true;

          if (res.listAssetIncrease.length == 0) {
            res.getEmty = true;
          }

          res.$refs.BaseLoading_ref.hide();
          res.listAssetIncreaseId = [];
          res.paging.amountPage = response.data.totalPage;

          res.listAssetIncrease.forEach((element, index) => {
            res.totalPriceEach[index] = 0;
            // duyệt qua tất cả các bản ghi

            res.listAssetIncrease[index].increaseDetail = JSON.parse(
              element.increaseDetail
            );
            res.listAssetIncrease[index].increaseDetail.forEach((item) => {
              typeof item.originalPrice === "string"
                ? (item.originalPrice = this.fomatMoneyToNumber(
                    item.originalPrice
                  ))
                : item.originalPrice;
              res.totalPriceEach[index] += item.originalPrice;
            });
            res.listAssetIncreaseId.push(element.assetIncreaseId); // push tất cả id tài sản vào mảng
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
    },

    // async getAssetIncrease() {
    //   await axios.get("http://localhost:56697/api/v1/");
    // },

    /// todo hiển thị dialog thêm
    showDialog(text, Id) {
      document.getElementById("ctxMenu").style.display = "none";
      if (text == "insert") {
        this.formMode = "insert";
        this.alerMsg = "Thêm mới thành công";
      } else {
        this.formMode = "update";
        this.alerMsg = "Cập nhật thành công";
        this.assetIdUpdate = Id;
      }
      setTimeout(() => {
        this.$refs.ModalCreateAsset_ref.show();
      }, 300);
      // debugger; // eslint-disable-line no-debugger
    },

    // todo tải lại dữ liệu
    reload(value) {
      if (value == true) {
        this.getAssetIncrease("");
        this.showSuccess = true;
        setTimeout(() => {
          this.showSuccess = false;
        }, 3000);
      }
    },

    //todo hiển thị form xác nhận xóa
    showDeleteDialog(text, id, e) {
      // var res = this

      this.alerMsg = "Xóa thành công!";
      if (text != "inRow" && this.listSelectRow.length == 0) {
        this.showWarning = true;
        this.alerMsg = "Vui lòng chọn bản ghi";
      } else if (text == "inRow") {
        this.formMode = "delete";

        this.indexDetail = this.listAssetIncreaseId.indexOf(id);
        this.increaseDetails = this.listAssetIncrease[
          this.indexDetail
        ].increaseDetail;

        document.getElementsByClassName("checkbox1").forEach((i) => {
          i.checked = false;
        });
        e.target.parentElement.parentElement.previousSibling.previousSibling.previousSibling.previousSibling.previousSibling.previousSibling.previousSibling.firstElementChild.checked = true;

        this.listSelectRow = [];
        this.listSelectRow.push(id);
        this.$refs.ModalDeleteAsset_ref.show();
      } else {
        this.formMode = "delete";

        this.$refs.ModalDeleteAsset_ref.show();
        this.showWarning = false;
      }
      setTimeout(() => {
        this.showWarning = false;
      }, 3000);
    },

    //  select hàng, nếu hàng đã được select thì xóa khỏi mẩng listSelectRow, và ngược lại
    selectRow(ind, e, id, text) {
      document.getElementsByClassName('checkboxAll1')[0].checked = false
      if (e.shiftKey) {
        var idFirst = this.listSelectRow[0];
        this.listSelectRow = [];
        this.listSelectRow.push(idFirst);

        // vị trí đầu tiên trong mảng listSelectRow
        var idStart = this.listSelectRow[0];
        var indexStart = this.listAssetIncreaseId.indexOf(idStart);

        var indexEnd = this.listAssetIncreaseId.indexOf(id);
        if (indexStart > indexEnd) {
          indexStart--;
          var tem = indexStart;
          indexStart = indexEnd;
          indexEnd = tem;
        } else {
          indexStart++;
        }
        for (var i = indexStart; i <= indexEnd; i++) {
          this.listSelectRow.push(this.listAssetIncreaseId[i]);
        }
      } else if (e.ctrlKey) {

        this.indexDetail = ind;
          this.increaseDetails = this.listAssetIncrease[
            this.indexDetail
          ].increaseDetail;

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
            this.indexDetail = this.listAssetIncreaseId.indexOf(
              this.listSelectRow[0]
            );
            this.increaseDetails = this.listAssetIncrease[
              this.indexDetail
            ].increaseDetail;
          }
        } else {
          document.getElementsByClassName("checkbox1").forEach((el) => {
            el.checked = false;
          });

          this.indexDetail = ind;
          this.increaseDetails = this.listAssetIncrease[
            this.indexDetail
          ].increaseDetail;
          this.listSelectRow = [];
          this.listSelectRow.push(id);
        }
      }
    },
    // todo xử lý sự kiện mũi tên lên xuống để select row
    processkey() {
      var res = this;
      document.addEventListener("keydown", function (e) {
        if (res.formMode == "") {
          var len1 = res.listSelectRow.length; // số phần tử của mảng listSelectRow
          var len2 = res.listAssetIncreaseId.length; //số phần tử của mảng listAssetId
          switch (e.keyCode) {
            case 38:
              {
                //up arrow
                if (
                  len1 == 0 ||
                  res.listAssetIncreaseId.indexOf(res.listSelectRow[0]) == 0
                ) {
                  res.listSelectRow = [];
                  res.listSelectRow.push(res.listAssetIncreaseId[len2 - 1]);
                } else if (len1 > 0) {
                  //res.listSelectRow = [];
                  var indexIdFirst = res.listAssetIncreaseId.indexOf(
                    res.listSelectRow[0]
                  );
                  res.listSelectRow = [];
                  res.listSelectRow.push(
                    res.listAssetIncreaseId[indexIdFirst - 1]
                  );
                }
              }
              break;
            case 40:
              {
                // //down arrow
                if (
                  len1 == 0 ||
                  res.listAssetIncreaseId.indexOf(
                    res.listSelectRow[len1 - 1]
                  ) ==
                    len2 - 1
                ) {
                  res.listSelectRow = [];
                  res.listSelectRow.push(res.listAssetIncreaseId[0]);
                } else if (len1 > 0) {
                  //res.listSelectRow = [];
                  var indexIdLast = res.listAssetIncreaseId.indexOf(
                    res.listSelectRow[len1 - 1]
                  );
                  res.listSelectRow = [];
                  res.listSelectRow.push(
                    res.listAssetIncreaseId[indexIdLast + 1]
                  );
                }
              }
              break;
            default: {
              return true;
            }
          }
        }
      });
    },
    // kiểm tra hàng đã được select hay chưa
    selectedRow(id) {
      if (this.listSelectRow.indexOf(id) > -1) return true;
      else return false;
    },

    // todo hiện và thao tác với context menu
    showContexMenu(id, e) {
      this.listSelectRow = [];
      this.listSelectRow.push(id);
      var ctx = document.getElementById("ctxMenu");
      ctx.style.display = "block";
      ctx.style.top = (e.screenY - 70).toFixed() + "px";
      ctx.style.left = e.screenX.toFixed() + "px";
    },

    // todo chuyển đến trang sau
    nextPage() {
      if (this.paging.pageNumber < this.paging.amountPage) {
        this.paging.pageNumber++;
        this.getAssetIncrease();
      }
    },
    // todo chuyển đến trang trước
    backPage() {
      if (this.paging.pageNumber > 1) {
        this.paging.pageNumber--;
        this.getAssetIncrease();
      }
    },

    // todo thay đổi khung nhìn
    changeVisual(text) {
      if (text == "all") {
        document.getElementsByClassName("content-grid-all")[0].style.height =
          "calc(100% - 109px)";

        document.getElementsByClassName(
          "grid-detail-container"
        )[0].style.display = "none";
        this.viewAll = true;
        document.getElementsByClassName("option-full")[0].style.background =
          "#f5f5f5";
        document.getElementsByClassName("option-detail")[0].style.background =
          "white";
      } else {
        this.viewAll = false;
        document.getElementsByClassName("grid-detail-container")[0].style.height =
          "calc(40% - 109px)";
          
        document.getElementsByClassName("content-grid-all")[0].style.height =
          "60%";

        document.getElementsByClassName(
          "grid-detail-container"
        )[0].style.display = "block";
        document.getElementsByClassName("option-full")[0].style.background =
          "white";
        document.getElementsByClassName("option-detail")[0].style.background =
          "#f5f5f5";
      }
    },
    // todo check tất cả các check box
    checkAll() {
      var res = this;
      this.listSelectRow = []
      if (document.getElementsByClassName("checkboxAll1")[0].checked == true) {
        this.listAssetIncreaseId.forEach((id) => {
          res.listSelectRow.push(id);
        });
        document.getElementsByClassName("checkbox1").forEach((element) => {
          element.checked = true;
        });
      } else {
        res.listSelectRow = [];
        document.getElementsByClassName("checkbox1").forEach((element) => {
          element.checked = false;
        });
      }
    },
    // todo thay đổi kích thước bảng detail
    resizeDetail(event) {

      console.log(this.resizing);
      this.resizing = true;
      console.log(this.resizing);

     
      var tableAssetIncrease = document.getElementsByClassName(
        "content-grid-all"
      )[0];

        var container =  document.getElementsByClassName ('div-container')[2];
        var containerDetail =  document.getElementsByClassName ('grid-detail-container')[0];

      window.addEventListener("mousemove", mousemove);
      window.addEventListener("mouseup", mouseup);

      var prevY = event.clientY;

      function mousemove(event) {
        if (!this.resizing) {
          var rect2 = tableAssetIncrease.getBoundingClientRect();
          var rect3 = containerDetail.getBoundingClientRect();
          
          var rect4 = container.getBoundingClientRect();

          var percent3 = parseFloat(rect3.height/rect4.height*100)
          var percent2 = parseFloat(rect2.height/rect4.height*100)

          var condition = percent3 + parseFloat((prevY - event.clientY)/rect4.height*100)
          if(condition > 27 && condition < 81)
          {
              containerDetail.style.height = "calc("+ percent3.toFixed()+"% + " +  (prevY - event.clientY).toFixed() + "px)";
            tableAssetIncrease.style.height = "calc("+ percent2.toFixed()+"% - " +  (prevY - event.clientY).toFixed() + "px)";
          }

          prevY = event.clientY;
        }
      }
      function mouseup() {
        window.removeEventListener("mousemove", mousemove);
        window.removeEventListener("mouseup", mouseup);
      }
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

    // định dạng ngày
    formatDate(inputDate) {
      var a = new Date(inputDate);
      var month = a.getMonth();
      var day = a.getDate();
      if (month < 10) month = "0" + month.toString();
      if (day < 10) day = "0" + day.toString();
      var date = day + "/" + month + "/" + a.getFullYear().toString();
      return date;
    },
  },
  updated() {
    document
      .getElementsByClassName("checkbox1")
      .forEach((ele) => (ele.checked = false));
    document.getElementsByClassName("selected-row").forEach((ele) => {
      ele.firstElementChild.firstElementChild.checked = true;
    });
  },
  beforeCreate() {
    this.indexDetail = 0;
  },
  created() {
    //this.getAssetIncrease();
  },
  mounted() {
    this.getAssetIncrease();
    this.processkey();
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
      background-color: #eaeaea;
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

.selected-row {
  background: #8dd2f369 !important;
}

.v-sheet.v-alert {
  position: absolute;
  z-index: 1001;
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

table tbody tr td {
  font-family: "GoogleSans";
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
  background-image: url("../../assets/icon/btn-firstpage.svg");
}

.prev-page {
  background: url(../../assets/icon/common-icon.png) no-repeat -700px -124px;
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
  background: url(../../assets/icon/common-icon.png) no-repeat -649px -124px;
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
  background: url(../../assets/icon/common-icon.png) no-repeat -849px -124px;
  border: 1px solid #6b6f9d !important;
}

.leftchild .last-page {
  background: url(../../assets/icon/common-icon.png) no-repeat -798px -124px;
}

.leftchild .next-page {
  background: url(../../assets/icon/common-icon.png) no-repeat -749px -124px;
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
.table-icon.icon-refresh-time {
  background-size: 20px !important;
}

.select-quantitypage {
  appearance: none;
  background: url(../../assets/icon/arrow-down-line.png) no-repeat;
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
  background-image: url("../../assets/icon/add.png") !important;
  background-size: 16px;
  background-repeat: no-repeat;
  background-position: center;
  height: 28px;
  width: 28px;
}
.features-pane-top {
  height: 34px;
  background: green;
  width: 100%;
}

.content-nav {
  padding: 10px;
  height: auto;
  margin: 0 16px;
}
.content-nav .features-pane {
  margin: 0px;
}
.content-grid {
  margin: 0 16px;
}
.panel-feature-top {
  height: 54px;
  position: relative;
  padding: 10px 16px;
}
.panel-feature-top .panel-feature {
  height: 100%;
  display: flex;
}
.panel-feature-top .panel-feature .feature-left {
  height: 100%;
  display: flex;
  align-items: center;
  padding-left: 8px;
  font-weight: 700;
}
.panel-feature-top .panel-feature .feature-right {
  position: absolute;
  right: 0;
  display: flex;
  align-items: center;
}
.button-refresh {
  padding-left: 20px;
}

.button-refresh .icon {
  height: 34px;
  width: 34px;
  background: url(../../assets/icon/qlts-icon.svg) no-repeat -103px -61px;

  background-repeat: no-repeat;
  border-radius: 4px;
}
.button-refresh .icon:hover {
  border: 1px solid #b1adad;
}

.input-search {
  padding: 8px 46px 8px 43px;
  font-size: 12px;
}

.content-nav .features-pane .features-pane-left .icon-search {
  position: absolute;
  /* right: 45px; */
  left: 0px;
}
.content-nav .features-pane .features-pane-left {
  position: relative;
}
.content-nav .features-pane input {
  width: 236px;
}
.content .div-container {
  background: #f5f6fa;
}
.content-nav {
  background: white;
}
table thead tr th {
  background: #f5f6fa;
}
table tbody {
  background: white;
}
.feature-right .button-option-view {
  height: 34px;
  width: 68px;
  margin-left: 10px;
  box-sizing: border-box;
  background: white;
  border-radius: 3px;
  padding-left: 14px;
  display: flex;
  align-items: center;
}
.panel-feature-top .panel-feature {
  height: 100%;
  display: flex;
  position: relative;
}

.panel-feature-top .panel-feature .icon-arrow {
  background-image: url("../../assets/UI/Icon/i_arrow_up_small.svg");
  background-repeat: no-repeat;
  background-size: 9px;
  transform: rotate(0deg);
  background-position: center;
  width: 10px;
  height: 10px;
  opacity: 0.6;
  margin-left: 12px;
}

.feature-right .button-option-view .menu-option-view {
  height: 52px;
  width: 68px;
  margin-left: 10px;
  box-sizing: border-box;
  background: white;
  border-radius: 3px;
  display: flex;
  align-items: center;
  position: absolute;
  right: 0;
  display: flex;
  flex-direction: column;
  top: 35px;
  z-index: 1;
  box-shadow: 2px 2px 10px #c3c3c3;
  padding-top: 5px;
}
.icon-view-all {
  height: 20px;
  width: 20px;
  background-image: url("../../assets/icon/view-full.png");
  background-repeat: no-repeat;
  background-size: contain;
  position: absolute;
  right: -9px;
}

.icon-view-detail {
  height: 20px;
  width: 20px;
  background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABYAAAAZCAYAAAA14t7uAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAEuSURBVEhL7ZIxi4MwGIbvX1fo5OLWycmCqIsILgodOhREBN0cFBwchDrUqVAQKqL1PSKh11ykl+EObvCBd8v78OVLPvBHrOInnPjxeOB+v6NtW6EMw0CbLIx4HEekaQrTNKFpmlBc10Xf99TwBTfx7XZDlmVIkkQodV3TJgsjnqYJ1+sVZVmiKAqhnM/neX3fYcTkAJH6vg/HcYQSBMHinrlVdF2HpmnmK4qErG4JTpznOWzbhmEYQvE8jzZZOPHhcICiKLAsa/Hqr1FVFdvtljZZODGZQNd1XC6X+T+/SxiGkCSJNlkWxeSK5Hf8RBzH/0S83+9RVdUsf5fT6YTNZkObLJz4eDzOj0fkr6+/lN1uB1mWaZOFE5NJiZxMLpIoimiThRP/Fqv4ySqmAJ+v/hYupF8OGwAAAABJRU5ErkJggg==);
  background-repeat: no-repeat;
  background-size: 28px 28px;
  background-position: center;
  position: absolute;
  right: -9px;
}
.panel-feature-top .panel-feature .icon-chosen {
  height: 20px;
  width: 20px;
  background-image: url("../../assets/icon/chosen-view.png");
  background-repeat: no-repeat;
  background-size: contain;
}
.panel-feature-top .panel-feature .item-option {
  display: flex;
  cursor: pointer;
}
.panel-feature-top .panel-feature .icon-view-detail {
  position: absolute;
  right: -9px;
}
.panel-feature-top .panel-feature .item-option {
  display: flex;
  width: 40px;
  position: relative;
}

.option-detail {
  margin-top: 5px;
}

.panel-feature-top .panel-feature .item-option {
  width: 100%;
  height: 50%;
  position: relative;
}
.panel-feature-top .panel-feature .icon-chosen {
  height: 20px;
  width: 20px;
  background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABYAAAAWCAYAAADEtGw7AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAKpSURBVEhL1ZTfS1NhGMf7N7oMoruwbsIiCKIUL4RCCOwuvFBKKA203zbN8EdFJKWUXRTO1mJz0zZ/m87MmmNay9zS1jbd3OZ+nZ3t+tv7vLHDzs4xIojo4nPxvs/zfN/3fJ/nPTuy2Sz+Bv9WOJ5OIpiMIiUKqnE1thVOpFMY9znR7RrG1Q99qJt7iib7C/Quj8EWcKnW5KMqHEhEcMuhx3HLTZSP3EaNrQcN889QNf0QZdYWvv/g4xBCyS1FbQ6FsD8RxsmxNhwyX0aPawTO0Cq8sRDW2WFrsQ3Ygx7cWTRhn/EiamcfY4NZVKhBKITpk3frz2HCt8jtKIwTMea5YXUORYZ6tDpeqebIhLUr09ijr8XA2rwsSY2UmGZ2vMYBUwOGvQ5FXCZ8drYHx5h/+Xv5JNICdB4bvzGtXeHv3O8Op1GRKwkn2SgdGbqGG/Z+vs5kMtgSEtxfgd2OGqVZ0KHYfAnvgys8h/pxfq4X1TPdPCenRUjC7qifF9E40VrMiNznM2+6YFx9h+aFlyixaNDnnpZENlMxtDsNOD15D774piRKSMLU3cODV9C5OMDXIruxI/SVTUg79rMJKBtuwfOVKcSEnzbkamjG6fBIKi7tEzKPK9nJlZN3pTW9tCn/EhrZDNNNC4vd0QAqxjpw3a6V7RMy4S7W5SJDHd6uL0t7JE4zTH7n5wqiiH73DA6yedd7ZmUxQibsZz5VjLej1NoMz1ZAllgI2VRi1aDa9oj/RwrjMmGCCuiBkKfzG19ksRzjrKnF5kYctTThU9irmqMQJtzstqXsNrt0NbzjTz6PwsQeDVlVPtKKndoqnJroxLdYUFGbQ1WYiApx3F8axInRNvZfqMdewwU+HdRcOkitJp9thfMhD5cjPkUDf8VvCf8J/5twFj8AZJXbCDaq+F8AAAAASUVORK5CYII=);
  background-repeat: no-repeat;
  background-size: contain;
  position: absolute;
  left: 12px;
}
.panel-feature-top .panel-feature .icon-view-all {
  right: 4px;
}
.panel-feature-top .panel-feature .icon-view-detail {
  position: absolute;
  right: 4px;
}

.menu-option-view .item-option:hover {
  background: #f5f5f5;
}
.menu-option-view .option-full {
  background: #f5f5f5;
}
.panel-feature-top .panel-feature .icon-view {
  position: inherit;
}
.menu-option-view {
  display: none !important;
}
.button-option-view:hover .menu-option-view {
  display: block !important;
}
.content-nav .features-pane input {
  border-radius: 2px;
}

.icon-print {
  background: url(../../assets/icon/qlts-icon.svg) no-repeat -324px -60px;
}
.icon-print:hover {
  background-color: rgba(214, 214, 214, 0.6);
}
.icon-print {
  background-repeat: no-repeat;
  opacity: 0.8;
}
.content-nav .features-pane .btn {
  padding: 16px !important;
  /* width: 34px; */
}
.icon-more {
  background: url(../../assets/icon/qlts-icon.svg) no-repeat -368px -17px;
  background-repeat: no-repeat;
}
.icon-more:hover {
  background-color: rgba(214, 214, 214, 0.6);
}
.content-nav {
  background: white;
  border: 1px solid #d0cece;
  box-shadow: 0px 0px 20px #bbb6b6;
  box-sizing: border-box;
  border-bottom: none;
}
.content-grid {
  margin: 0 16px;
  box-sizing: border-box;
  border: 1px solid #d0cece;
}
table tbody tr td {
  border-right: none;
}

.feature-right .button-option-view .menu-option-view {
  top: 33px;
}
.content-grid {
  height: calc(100% - 315px);
}
.content-grid-detail {
  height: 175px;
}
.header-table-detail {
  height: 0px;
  display: flex;
  align-items: center;
  padding-left: 16px;
}
.header-table-detail {
  position: fixed;
  z-index: 2;
  width: 100%;
  padding-right: 16px;
}
.table-detail thead tr th {
  text-transform: uppercase;
  font-size: 11px;
  color: #1d3464;
  position: sticky;
  top: 40px;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}
.table-detail tbody {
  margin-top: 38px;
}
.header-table-detail {
  position: fixed;
  z-index: 2;
  /* width: 87%; */
  /* width: 324px; */
  width: 78%;
  background: white;
  padding-right: 16px;
}
.table-summary {
  bottom: -20px;
}
.menu-option-view .option-detail {
  background: #f5f5f5;
}
.menu-option-view .option-full {
  background: white;
}

.header-table-detail {
  position: fixed;
  z-index: 2;
  /* width: 87%; */
  /* width: 632px; */
  width: calc(100% - 244px);
  padding: 0;
  /* width: 200px; */
  /* width: auto; */
  box-sizing: border-box;
  background: white;
  /* padding-right: 73px; */
  /* padding-right: 26px; */
}
.header-table-detail .text {
  height: 100%;
  width: 100%;
  display: flex;
  align-items: center;
  padding-left: 16px;
}
.table-summary {
  bottom: -28px;
}

.checkbox-fake {
  height: 16px;
  width: 16px;
  background: red;
  position: absolute;
}
.checkbox-content {
  height: 16px;
  width: 16px;
  background: red;
  position: absolute;
}
.table-detail tr td:last-child,
.table-detail thead th:last-child {
  padding-right: 30px;
}
.resize-icon {
  width: 50px;
  height: 14px;
  position: absolute;
  top: -3px;
  left: 48%;
  cursor: n-resize;
}
.resize-icon .icon-top {
  border-top: 1px solid #686868;
  width: 84%;
}
.resize-icon .icon-bottom {
  border-top: 1px solid #686868;
  width: 84%;
}
.resize-icon .icon-center {
  width: 100%;
  height: 5px;
  background: #686868;
  border-radius: 5px;
  margin: 1px 0;
  margin-left: -4px;
}
.header-table-detail.text-title {
  border-top: 4px solid #d0cece;
}
.content-grid-detail {
  height: 175px;
  min-height: 175px;
}

.icon-edit-pen1 {
  height: 26px !important;
  width: 34px !important;
  background: url(../../assets/icon/qlts-icon.svg) no-repeat -326px -60px;
  background-repeat: no-repeat;
  border-radius: 4px;
}
.icon-trash-table1 {
  height: 26px !important;
  width: 34px !important;
  background: url(../../assets/icon/qlts-icon.svg) no-repeat -148px -61px;
  background-repeat: no-repeat;
  border-radius: 4px;
}
.icon-refresh-time1 { 
  height: 26px !important;
  width: 34px !important;
  background: url(../../assets/icon/qlts-icon.svg) no-repeat -454px -104px;
  background-repeat: no-repeat;
  border-radius: 4px;
}

.table-summary {
  height: 55px;
  /* margin-top: 6px; */
  margin-left: 0;
  padding-top: 8px;
  background: #f5f6fa;
  padding-left: 36px;
  /* border-top: 1px solid #e5e5e5; */
  /* margin-top: -9px; */
}
.button-refresh[data-v-5ecf59db] {
  padding-left: 4px;
}
.paging-toolbar .leftchild {
  height: 100%;
  min-width: 330px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 355px;
}
input.checkbox1.checkboxAll1 {
  position: absolute;
  left: 16px;
  top: 10px;
}
.content {
  height: calc(100%);
}
.table-detail thead tr th[data-v-5ecf59db] {
  text-transform: uppercase;
  font-size: 11px;
  color: #1d3464;
  position: sticky;
  top: 0;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}
.title {
  padding: 12px;
  margin: 0 16px;
  width: calc(100% - 32px);
  border-top: none;
  box-sizing: border-box;
  background: white;
  border-left: 1px solid #d0cece;
  border-right: 1px solid #d0cece;
}
.content-nav {
  box-shadow: none;
}

.header-table-detail.text-title {
  border-top: 4px solid #d0cece;
  margin-left: 16px;
}
.content-grid {
  overflow-x: hidden;
}
.content-grid-all {
  height: calc(100% - 344px);
}
.title {
  border-top: 4px solid #e5e5e5;
}
.header-table-detail.text-title {
  border-top: none;
}
.icon-trash-table1[data-v-5ecf59db] {
  height: 26px !important;
  width: 34px !important;
  background: url(/img/qlts-icon.d656886f.svg) no-repeat -121px -102px;
  background-repeat: no-repeat;
  border-radius: 3px;
  background-size: 428px 1217px;
}
.icon-edit-pen1[data-v-5ecf59db] {
  height: 26px !important;
  width: 34px !important;
  background: url(/img/qlts-icon.d656886f.svg) no-repeat -306px -99px;
  background-repeat: no-repeat;
  border-radius: 4px;
  background-size: 480px 1332px;
}
.icon-refresh-time1[data-v-5ecf59db] {
  height: 26px !important;
  width: 34px !important;
  background: url(/img/qlts-icon.d656886f.svg) no-repeat -396px -121px;
  background-repeat: no-repeat;
  border-radius: 4px;
  background-size: 440px 1207px;
}
.button-refresh .icon[data-v-5ecf59db] {
  height: 34px;
  width: 34px;
  background: url(/img/qlts-icon.d656886f.svg) no-repeat -109px -49px;
  background-repeat: no-repeat;
  border-radius: 4px;
  background: -s;
  background-size: 473px 1110px;
}
.header-right-item {
  display: none;
}
.icon-trash {
  background: url(/img/qlts-icon.d656886f.svg) no-repeat -453px -105px;
}
.v-sheet.v-alert {
  bottom: 35px;
}
.table-detail th#columnDepartment {
  width: 300px;
  max-width: 300px;
}
.content-grid-detail.content-grid.grid {
  border-top: none;
}
.table-detail thead tr th {
  border-top: 1px solid #d6d6d6;
  background-color: #f5f6fa;
}



.content-grid-detail{
    height: 175px;
    min-height: 175px;
}

.content {
    height: calc(100% - 40px);
}
.grid-detail-container {
    height: 25.3%;
}

.header-table-detail.text-title{
    border-top: 2px solid #dedede;
}
.header .header-left .parent-section[data-v-9778edae] {
    color: #178aa9;
    font-family: GoogleSans-Regular;
}
.resize-icon {
    width: 50px;
    height: 14px;
    position: absolute;
    top: -6px;
    left: 48%;
    cursor: n-resize;
}


//resize

.content-grid-all {
    height: 60%;
}
.grid-detail-container {
    height: calc(40% - 109px);
}
.content-grid-detail {
    height: calc(100% - 60px);
    min-height: calc(100% - 60px);
}
.resize-icon {
    width: 50px;
    height: 27px;
    position: absolute;
    top: -15px;
    left: 48%;
    cursor: n-resize;
    padding-top: 9px;
}
.table-increase tr td:nth-child(3)
{
  color: #00abfe;
}
.table-increase tr td:nth-child(3):hover{
text-decoration: underline;
}
.title{
font-size: 16px!important;
}
.text-title{
  font-size: 20px;
}
.btn-add-asset {
    padding: 0 36px;
    color: white;
    background-color: #1BA3C9;
    border: none;
}

</style>