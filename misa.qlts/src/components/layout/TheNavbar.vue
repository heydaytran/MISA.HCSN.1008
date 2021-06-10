<template>
  <div class="div-container nav" id="navbar">
    <div class="nav-item header">
      <div class="icon-nav logo-page" @click="toggleNavbar('icon')"></div>
      <div class="nav-item-text nav-title">QLTS.VN</div>
      <div class="icon-hamburger-menu" @click="toggleNavbar('no')"></div>
    </div>
    <div id="navItem1" class="nav-item option nav-item-icon">
      <div class="icon-nav icon-computer-line"></div>
      <div class="nav-item-text">Tổng quan</div>
    </div>
    <div id="navItem2" class="nav-item option nav-item-icon">
      <div class="icon-nav icon-community-line"></div>
      <div class="nav-item-text" > Tài sản</div> 
      <div class="icon-submenu">

      </div>
      <div class="sub-navbar">
        <router-link class="sub-navbar-item" to="/asset/increase" tag="div"> Ghi tăng</router-link> 
        <div class="sub-navbar-item">Điều chuyển</div>
        <div class="sub-navbar-item">Tính hao mòn</div>
        <div class="sub-navbar-item">Ghi giảm</div>
        
      </div>
    </div>
    <div id="navItem3" class="nav-item option nav-item-icon">
      <div class="icon-nav icon-paint-brush-line"></div>
      <div class="nav-item-text">Công cụ dụng cụ</div>
      <div class="icon-submenu">

      </div>
      <div class="sub-navbar">
        <div class="sub-navbar-item">Sản phẩm sản xuất</div>
        <div class="sub-navbar-item">Dụng cụ</div>
      </div>
    </div>
    <div id="navItem4" class="nav-item option nav-item-icon option-focus">
      <div class="icon-nav icon-common"></div>
      <div class="nav-item-text">Danh mục</div>
      <div class="icon-submenu">

      </div>
      <div class="sub-navbar">
         <router-link class="sub-navbar-item" to="/asset" tag="div"> Tài sản</router-link> 
        <div class="sub-navbar-item">Vật liệu</div>
        <div class="sub-navbar-item">Nguyên liệu</div>
      </div>
    </div>
    <div id="navItem5" class="nav-item option nav-item-icon">
      <div class="icon-nav icon-search-eye"></div>
      <div class="nav-item-text">Tra cứu</div>
    </div>
    <div id="navItem6" class="nav-item option nav-item-icon">
      <div class="icon-nav icon-setting"></div>
      <div class="nav-item-text">Hệ thống</div>
    </div>
    <div id="navItem7" class="nav-item option nav-item-icon">
      <div class="icon-nav icon-pie-chart-line"></div>
      <div class="nav-item-text">Báo cáo</div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      collapseNavbar: true,
    };
  },
  methods: {
    toggleNavbar(text) {
      this.collapseNavbar = !this.collapseNavbar;
      var navbar = document.getElementById("navbar");
      var bodyRight = document.getElementsByClassName("body-right")[0]
      var itemText = document.getElementsByClassName("nav-item-text")
      var iconSubmenu = document.getElementsByClassName("icon-submenu")

      if (text == "icon" && this.collapseNavbar == true) {
        
        navbar.style.width = "210px"
        bodyRight.style.marginLeft =
          "210px";
          bodyRight.style.width = "calc(100% - 210px)"

        document.getElementsByClassName(
          "icon-hamburger-menu"
        )[0].style.display = "block"

        itemText.forEach((element) => {
          element.style.display = "block";
        });

         iconSubmenu.forEach((element) => {
            element.style.border="5px solid"
          element.style.bottom = "auto";
          element.style.right = "12px";
          element.style.borderColor = "transparent transparent transparent white"
        }); 
      } else {
        navbar.style.width = "60px"
        bodyRight.style.marginLeft =
          "60px";
          bodyRight.style.width = "calc(100% - 60px)"

        document.getElementsByClassName(
          "icon-hamburger-menu"
        )[0].style.display = "none"

        itemText.forEach((element) => {
          element.style.display = "none";
        }); 
          iconSubmenu.forEach((element) => {
            element.style.border="3px solid"
          element.style.bottom = "0";
          element.style.right = "2px";
          element.style.borderColor = "transparent white white transparent"
        }); 

      }
    },
  },
};
</script>


<style lang="scss" scoped>
@import url("../../style/scss/icon.scss");

.nav {
  // float: left;
  background-color: #011239;
  color: rgb(5, 12, 75);
  font-size: 14px;
  width: 210px;
  height: 100vh;
  box-sizing: border-box;
  border-right: #e2e1e6 1px solid;
  position: fixed;

  top: 0;
  left: 0;
  transition: width 0.5s;
  color: white;

  .nav-item {
    height: 60px;
    display: flex;
    box-sizing: border-box;
    align-items: center;
    padding-left: 13px;
    position: relative;

    &.option-focus {
      border-left: 4px solid #00abfe;
      background-color: #273557;
      outline: none;
      padding-left: 9px !important;
      transition: 0.2s;
    }

    &.header {
      margin-bottom: 2px;
      padding-left: 6px;

      .icon-hamburger-menu {
        padding-left: 40px;
        background-position: right;
      }

      &:hover {
        cursor: pointer;
      }
    }

    &.nav-item-icon:hover {
      background-color: #273557;
      cursor: pointer;
      transition: 0.2s;

      .sub-navbar {
        display: block;
      }
    }

    &.option {
      padding-left: 0;
      transition: all 0.5s ease;
      padding: 0 9px;
    }

    .nav-title {
      font-weight: bold;
      font-family: GoogleSans-Bold;
      text-transform: uppercase;
      font-size: 24px;
      padding-left: 6px !important;
      transition: width 0.5s;

      &:hover {
        cursor: pointer;
      }
    }
    .sub-navbar {
      // position: relative;
      // right:0;
      position: absolute;
      left: 100%;
      background-color: white;
      color: black;
      width: fit-content;
      box-shadow: 2px 2px 10px #c3c3c3;
      top: 0;
      display: none;

      .sub-navbar-item {
        padding: 10px 60px 10px 22px;
        &:hover {
          cursor: pointer;
          background-color: #eaeaea;
        }
      }
    }

    .nav-item-text {
      padding-left: 8px;
      transition: none;
    }
  }
}

.nav .nav-item.header .icon-hamburger-menu {
  padding-left: 40px;
  box-sizing: border-box;
  margin: 0px;
  background-position: right !important;
}
.nav .nav-item .sub-navbar {
    white-space: nowrap;
}
.nav:hover{
  z-index: 1;
}
.nav:hover .sub-navbar{
  z-index: 1;
}
.icon-submenu{
    border: 5px solid;
    border-color: transparent transparent transparent white;
    position: absolute;
    right: 12px;
}
.nav .nav-item {
    height: 48px;
    display: flex;
    box-sizing: border-box;
    align-items: center;
    padding-left: 13px;
    position: relative;
}

.nav-item:hover .icon-submenu
{
  border-color:transparent transparent transparent #00abfe;
}


</style>