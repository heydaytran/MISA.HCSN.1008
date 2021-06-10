import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../components/HelloWorld.vue'
import Asset from '../components/dictionary/AssetManagement.vue'
import IncreaseAsset from '../views/increaseAsset/IncreaseList.vue'

Vue.use(VueRouter)

export const router = new VueRouter({
    routes: [
        {path: '/', component: Home},
        {path: '/asset', component: Asset},
        {path:'/asset/increase', component:IncreaseAsset}
    ]
})