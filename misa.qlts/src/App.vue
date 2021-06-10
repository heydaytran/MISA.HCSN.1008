// <template>
  <v-app>
    <v-main>
      <div class="body-container">
        <Navbar />
        <div class="body-right">
          <Header @activeFilterMenu="activeFilterMenu" :optionFilter="optionFilter"/>
        <router-view tag="div" :isFilterMenu="isFilterMenu" @allAsset="allAsset" />
       </div>
      </div>
    </v-main>
  </v-app>
</template>

<script>
import Header from "./components/layout/TheHeader.vue";
import Navbar from "./components/layout/TheNavbar.vue";

export default {
  name: "App",

  components: {
    Header,
    Navbar,
  },
  methods: {
    activeFilterMenu(value) {
      this.isFilterMenu = value;
    },
    allAsset(value)
    {
      this.optionFilter = value
    }
  },

  data() {
    return {
      isFilterMenu: false,
      optionFilter:'Tất cả'
    };
  },
};
</script>



<style lang="scss" >
@import url("style/scss/common.scss");
@import url("style/scss/icon.scss");
@import url("style/scss/button.scss");
@import url("./style/scss/combobox.scss");
 @import url("style/scss/input.scss");
@import url("style/scss/table.scss");
@import url("style/scss/modal.scss");
@import url("style/scss/date_picker.scss");

@font-face {
  font-family: GoogleSans;
  src: url("./assets/font/GoogleSans-Thin.otf");
}
@font-face {
  font-family: GoogleSans-Bold;
  src: url("./assets/font/GoogleSans-Bold.otf");
}
@font-face {
  font-family: GoogleSans-Regular;
  src: url("./assets/font/GoogleSans-Regular.otf");
}
@font-face {
  font-family: GoogleSans-Medium;
  src: url("./assets/font/GoogleSans-Medium.otf");
}
.div-container {
  width: 100%;
  height: 100%;
}
body {
  margin: 0;
  font-family: GoogleSans;
  // font-family: 'GoogleSans';
}

::-webkit-scrollbar {
  width: 8px;
  height: 8px;
  display: block;
 }
// /* Track */
::-webkit-scrollbar-track:hover {
  background: rgb(235, 235, 235);
  // background: rgb(194, 194, 194);
  margin-top: 32px;
  border-radius: 10px;
}
/* Track */
::-webkit-scrollbar-track {
  background: transparent;
  margin-top: 32px;
}
/* Handle */
::-webkit-scrollbar-thumb {
  background: rgb(194, 194, 194);
  border-radius: 10px;
  // opacity: 0.5;
}
/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: rgb(131, 131, 131);
  /* filter: brightness(50%); */
}
.body-right {
  height: 100vh;
  width: calc(100% - 210px);
  z-index: 0;
}
.body-app {
  display: flex;
}
.body-left {
  height: auto;
  width: auto;
}

.body-right {
  /* width: calc(100% - 210px); */
  /* float: left; */
  margin-left: 210px;
  box-sizing: border-box;
  overflow: hidden;
  transition: all 0.5s;
}
.body-container {
  display: flex;
}
::-webkit-scrollbar-track {
    background: transparent;
    margin-top: 40px;
}
</style>