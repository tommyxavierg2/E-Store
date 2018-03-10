import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'
import VeeValidate from 'vee-validate';
import toastr from 'toastr';

toastr.options = { timeOut: 1000, positionClass: 'toast-top-right', showMethod: 'fadeIn', hideMethod: 'fadeOut' }
window.toastr = toastr

Vue.use(VeeValidate);

// Registration of global components
Vue.component('icon', FontAwesomeIcon);

Vue.prototype.$http = axios;

sync(store, router)

const app = new Vue({
    store,
    router,
    ...App
})

export {
    app,
    router,
    store
}