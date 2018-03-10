import HomePage from 'components/home-page'
import RegisterPage from 'components/register';
import LoginPage from 'components/login';
import DetailPage from 'components/detail';
import ProfilePage from 'components/profile';
import store from '../store/index';

export const routes = [
    { name: 'home', path: '/', component: HomePage },
    { name: 'register', path: '/register', component: RegisterPage },
    { name: 'login', path: '/login', component: LoginPage },
    { name: 'detail', path: '/detail/:id', component: DetailPage },
    { name: 'profile', path: '/profile/:id', component: ProfilePage },

]
