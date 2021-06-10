<template>
  <div class="div-container header" id="header">
    <div class="header-left">
      <div class="parent-section">Danh mục</div>
      <div class="icon-seperator"></div>
      <div class="child-section">Tài sản</div>
    </div>

    <div class="header-right">
      <div class="header-right-item" style="font-family: GoogleSans-Regular">
        Sở tài chính
      </div>
      <div class="header-right-item">
        <div class="combobox">
          <div class="combobox-text hover-pointer">{{ optionFilter }}</div>
          <div class="icon-updown hover-pointer" @click="showMenuFilter()"></div>

         

        </div>
      </div>
      <div class="icon-help header-right-item hover-pointer"></div>
      <div class="icon-bell header-right-item hover-pointer">
        <div class="notification">1</div>
      </div>
      <div class="icon-divider"></div>
      <div class="header-right-user">
        <div class="avatar hover-pointer">
          <img src="../../assets/icon/avatar-default.png" alt="" />
        </div>
        <div class="icon-down hover-pointer"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Header",
  props:{
    optionFilter:String
  },
  data() {
    return {
     
      activeFilterMenu : false
    };
  },
  methods:{
    showMenuFilter()
    {
      this.activeFilterMenu = !this.activeFilterMenu
      this.$emit("activeFilterMenu", this.activeFilterMenu)
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
@import url("../../style/_variables.scss");
.header {
  height: 60px;
  // height: $header-height;
  width: calc(100% - 200px);
  margin-left: 200px;
  box-sizing: border-box;
  border-bottom: #e2e1e6 1px solid;
  overflow: visible;
  // margin-left: $navbar-width-max;
  margin-left: 210px;
  transition: all 0.5s;

  .header-left {
    height: 60px;
    line-height: 60px;
    padding-left: 16px;
    font-weight: bold;
    font-size: 15px;
    display: flex;
    align-items: center;

    .parent-section {
       color: #178aa9;
      font-family: GoogleSans-Regular;

      &:hover {
        cursor: pointer;
      }
    }
  }

  .header-right {
    display: flex;
    height: 60px;
    align-items: center;
    .icon-bell {
      position: relative;
      .notification {
        width: 12px;
        height: 12px;
        position: absolute;
        background-color: #00abfe;
        color: white;
        right: 2px;
        border-radius: 50%;
        top: 2px;
        line-height: 12px;
        font-size: 8px;
        text-align: center;
      }
    }
    .header-right-user {
      display: flex;
      align-items: center;
      margin-right: 16px;
      .avatar {
        width: 32px;
        height: 32px;
        margin: 0 10px 0 6px;

        img {
          width: 100%;
          height: 100%;
        }
      }
    }

    .header-right-item {
      line-height: 60px;
      // padding: 6px;
      margin: 0 10px;
      font-weight: bolder;
    }

    .user-setting {
      height: 60px;
      padding: 0 6px;
      width: 50px;

      //   &.icon-more {
      //     background-image: url("../../assets/icon-more.png");
      //     background-repeat: no-repeat;
      //     background-position: center;
      //     background-size: 20px 20px;
      //   }
    }
  }
}

.header .header-right {
  position: absolute;
  right: 0;
}

.content {
  height: 60px;
  /* width: calc(100% - 200px); */
  position: relative;
  /* margin-left: 210px; */
  box-sizing: border-box;
  border-bottom: #e2e1e6 1px solid;
  /* overflow: visible; */
  margin-left: 210px;
  transition: all 0.5s;
  width: calc(100% - 210px);
}

.header {
  height: 60px;
  width: 100%;
  position: relative;
  display: flex;
  margin-left: 0px;
  box-sizing: border-box;
  border-bottom: #e2e1e6 1px solid;
  transition: all 0.5s;
}
.header .header-left {
  /* float: left; */
  float: none;
  height: 60px;
  line-height: 60px;
  padding-left: 16px;
  font-weight: bold;
  font-size: 15px;
  display: flex;
  align-items: center;
}
.combobox
{
  position: relative;
}
.icon-down {
  opacity: 0.6!important;
}
.header{
    height: 40px;
    align-items: center;
    width: 100%;
    position: relative;
    display: flex;
    margin-left: 0px;
    box-sizing: border-box;
    border-bottom: #e2e1e6 1px solid;
    transition: all 0.5s;
}
.header .header-right .header-right-user .avatar {
    width: 28px;
    height: 28px;
    margin: 0 10px 0 6px;
}
.combobox {
   
    height: 27px;
}
</style>
