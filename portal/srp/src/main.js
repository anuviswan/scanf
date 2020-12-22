import Vue from 'vue'
import App from './App.vue'
import router from "./router/index";
import Vuetify from "vuetify";
import "vuetify/dist/vuetify.min.css";
import store from './store'

Vue.config.productionTip = false

new Vue({
  vuetify : new Vuetify(),
  router,
  store,
  render: h => h(App)
}).$mount('#app')
