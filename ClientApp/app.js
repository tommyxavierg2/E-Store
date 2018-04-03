import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import { FontAwesomeIcon } from './icons'
import BootstrapVue from "bootstrap-vue";
import firebase from 'firebase';
import App from 'components/app-root'
import VeeValidate from 'vee-validate';
import toastr from 'toastr';
toastr.options = { timeOut: 1000, positionClass: 'toast-top-right', showMethod: 'fadeIn', hideMethod: 'fadeOut' }


const config = {
    apiKey: "AIzaSyBYMgP-MHbzVW32P8xX9zUY9m6IfiCmpAI",
    authDomain: "starbase-6d93c.firebaseapp.com",
    databaseURL: "https://starbase-6d93c.firebaseio.com",
    projectId: "starbase-6d93c",
    storageBucket: "",
    messagingSenderId: "654031031562"
};

firebase.initializeApp(config);
// Registration of global components

Vue.use(BootstrapVue);
Vue.use(VeeValidate);
Vue.component('icon', FontAwesomeIcon);
window.toastr = toastr

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