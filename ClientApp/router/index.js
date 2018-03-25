import Vue from 'vue'
import VueRouter from 'vue-router'

//Routes
import HomePage from 'components/home-page'
import RegisterPage from 'components/register';
import LoginPage from 'components/login';
import DetailPage from 'components/detail';
import ProfilePage from 'components/profile';
import ErrorPage from 'components/error404';

import store from '../store/index';

Vue.use(VueRouter);

function requireAuth(to, from, next) {
    if (!store.state.loggedIn) {
        toastr.warning('In order to access this section you need to be logged in');
        next({
            path: '/login'
        });
    } else if (to.path === '/detail/:id' || to.path === '/profile/:id') {
        next({
            path: '/error'
        });
    } else if (store.state.loggedIn && to.path === '/login' || to.path === '/register')  {
        next({
            path: '/'
        }) 
    } else {
        next();
    }
}

let router = new VueRouter({
    mode: 'history',
    routes: [
        { name: 'home', path: '/', component: HomePage },
        { name: 'register', path: '/register', component: RegisterPage },
        { name: 'login', path: '/login', component: LoginPage },
        { name: 'detail', path: '/detail/:id', component: DetailPage },
        { name: 'profile', path: '/profile/:id', component: ProfilePage, beforeEnter: requireAuth },
        { name: 'error', path: '/error', component: ErrorPage },
    ]
})

export default router
