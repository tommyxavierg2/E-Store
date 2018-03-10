import Vue from 'vue'
import Vuex from 'vuex'
import api from './api';

Vue.use(Vuex)

export default new Vuex.Store({
// STATE
    state: {
        loggedIn: false,
        user: {}
    }, 

// MUTATIONS
    mutations: {
        login(state, data) {
            state.user = data;
            state.loggedIn = true;
        },

        logout(state) {
            state.user = {};
            state.loggedIn = false;
            localStorage.removeItem('userData');
        }
    },

// ACTIONS
    actions: {
        
    }
});
