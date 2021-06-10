<template>
  <div>
    <div v-show="isActive" class="div-container modal">
      <div class="modal-background"></div>
      <div class="modal-content">
        <div class="modal-header">Xóa bản ghi
          <span @click="hide()"><div data-v-1b34bf39="" class="icon-cancel btn btn-close"></div></span>
        </div>
        <div class="modal-content" style="width: 318px">
          Dữ liệu các chứng từ đã chọn sẽ bị xóa. <br>
          Bạn có chắc chắn muốn xóa?
        </div>
        <div class="modal-footer">
          <div class="btn" @click="hide()" >Hủy</div>
          <div class="btn btn-confirm-delete" @click="confirmDelete()">Xóa</div>
        </div>
      </div>
         <BaseLoading ref="BaseLoading_ref"/>
    </div>
  </div>
</template>

<script>
import BaseLoading from '../../common/BaseLoading.vue'
import axios from "axios";

export default {
  components:{
    BaseLoading
  },
  props: {
    listSelectRow:Array
  },
  data() {
    return {
      isActive: false,
    };
  },
  methods: {
    show() {
      this.isActive = true;
    },

    hide() {
      this.isActive = false;
       this.$emit("statusForm", '');
    },


    // todo xác nhận xóa bản ghi
     async confirmDelete() {
      var res = this
      res.$refs.BaseLoading_ref.show()

      for (var i = 0; i < this.listSelectRow.length; i++) {
        await axios
          .delete(
            "https://localhost:44382/api/v1/AssetIncreases/" +
            this.listSelectRow[i]
          )
          .then((Response) => {
            res.$refs.BaseLoading_ref.hide()
            console.log(Response);
          })
          .catch((err)=>{
            console.log(err);
            alert("Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp")
            return
          })
      }
      res.hide()
      res.$emit("reload", true)
      document.getElementsByClassName('checkbox1').forEach(element => {
          element.checked = false
      });

    },
  },
};
</script>


<style lang='scss' scoped>
.btn.btn-confirm-delete {
  background-color: #ff4646;
  color: white;
}
.btn.btn-confirm-delete:hover {
  background-color: #ff7777;
}

.modal {
  .modal-content {
    position: relative;
    top: calc(50% - 100px);
    left: calc(50% - 175px);
    width: 350px;
    height: 200px;
    background-color: white;

    .modal-header {
      height: 60px;
      padding: 0 16px;
      line-height: 60px;
      font-size: 18px;
      font-weight: 500;
    }

    .modal-content {
      height: calc(100% - 110px);
      padding: 16px;
    }
    .modal-footer {
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

.modal .modal-content {
    border-radius: 4px;
}
.modal .modal-content .modal-header {
    border-bottom: 1px solid #e6e6e6;
}

.modal .modal-content .modal-footer {
    border-radius: 4px;
}
.modal .modal-content{
  height: auto;
}
.modal .modal-content .modal-header {
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

.modal .modal-content .modal-header{
    display: flex;
    align-items: center;
}
.icon-cancel.btn.btn-close {
    padding: 0!important;
}
</style>