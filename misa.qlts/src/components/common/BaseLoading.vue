<template>
    <div>
         <div v-show="getSuccess" class="loading-dialog">
            <div class="icon-loading"></div>
          </div>
    </div>
</template>

<script>
export default {
    data(){
        return{
            getSuccess:false
        }
    },
    methods:{
        hide()
        {
            this.getSuccess = false
        },
        show()
        {
            this.getSuccess = true
        }
    }
}
</script>

<style scoped>

.loading-dialog {
  position: fixed;
    opacity: 0.5;
    z-index: 200;
    top: 0;
    left: 0px;
    right: 0;
    bottom: 0;
    background: black;
    display: flex;
    align-items: center;
}

.loading-dialog .icon-loading {
  background-image: url("../../assets/UI/Icon/loading.svg");
      position: absolute;
    z-index: 5;
    height: 80px;
    width: 80px;
    background-size: contain;
    margin-left: calc((100% - 80px)/2);
}

.loading-emty {
  font-weight: bold;
  width: inherit;
  display: flex;
  height: 138px;
  justify-content: center;
  align-items: center;
  font-size: 24px;
  position: absolute;
  top: 0;
}
</style>