<template>
    <div id="app">

        <b-navbar type="dark" class="space">

            <b-navbar-brand>
                <router-link to="/" style="padding-left: 10%;">E-Store</router-link>
            </b-navbar-brand>

            <b-navbar-nav v-if="checkUserLogged.loggedIn">
                <router-link :to="{ name: 'profile', params: { id: checkUserLogged.user.id }}">
                    <icon icon="user-circle"/>Profile 
                </router-link>
            </b-navbar-nav>

            <b-navbar-nav class="ml-auto">

                <multiselect v-model="records.multiselect.searchValue" :options="records.multiselect.searchedProduct" :searchable="true" placeholder="¿What you looking for?" label="name" track-by="name" :loading="records.isLoading" :limit="5" @search-change="searchProduct" :options-limit="3" @select="goToProductDetails" class="form-control">
                    <template slot="option" slot-scope="props" style="color: black;"> 
                        <img class="option__image" :src="props.option.image" :alt="props.option.name" style="width: 20%; height: 10%;"> 
                            <router-link class="option__title" :to="{ path: `/detail/${props.option.id}`}" style="color: black;">{{props.option.name}}</router-link>
                    </template>   
                    <span slot="noResult">Oops! No elements found.</span>
                </multiselect>
                
            </b-navbar-nav>

            <b-nav-item-dropdown>
                <template slot="button-content">
                    <icon icon="list"/>
                    <em>Menu</em>
                </template>
                <router-link to="/settings" v-if="checkUserLogged.loggedIn"><icon icon="wrench"/>Settings</router-link>
                <b-dropdown-item to="/login" v-if="!checkUserLogged.loggedIn"> <icon icon="sign-in-alt"/>Login</b-dropdown-item>
                <b-dropdown-item to="/register" v-if="!checkUserLogged.loggedIn">Register</b-dropdown-item>
                <b-dropdown-item @click="logout()" v-if="checkUserLogged.loggedIn"> <icon icon="sign-out-alt"/> Logout</b-dropdown-item>
            </b-nav-item-dropdown>

        </b-navbar>

        <router-view class="container"></router-view>
    </div>
</template>

<script>
    import store from '../store/index';
    import {api} from '../store/api';   
    import Multiselect from 'vue-multiselect'

    export default {
        components: {
            Multiselect
        },

        data() {
            return {
                records: {
                    multiselect: { searchValue: '', isLoading: false, searchedProduct: [], }
                }
            }
        },

        computed: {
            checkUserLogged() {
                return this.$store.state;
            }
        },

        methods: {
            searchProduct(query) {
                if (query) {
                    this.records.multiselect.isLoading = true;
                    this.$http.get(`${api.url}product?state=1&name=${query}`)
                        .then(response => {
                            this.records.multiselect.searchedProduct = response.data;
                            this.records.isLoading = false;
                        })
                        .catch(err => alert(err));
                }
            },

            goToProductDetails(selected) {
                this.$router.push(`/detail/${selected.id}`);
            },

            logout() {
                if (confirm('¿Are you sure about logging out?')) {
                    this.$store.commit('logout');
                    this.$router.replace('/');
                    alert('See you soon');
                }
            },

            init() {
                if (!this.checkUserLogged.loggedIn) {
                    let userData = JSON.parse(localStorage.getItem('userData'));
                    if (userData) {
                        this.$store.commit('login', userData);   
                    }
                }
            }
        },

        created() {
            this.init();
        }
    }
</script>

<style>


    .navbar {
        display: flex; 
        background-color: #2980B9;
        width: 100%;

    }

    .navbar a {
        width: 100%;
        color: white;   
    }

    .nav-item a {
        color: black;
    }

    .space {
        margin-bottom: 20px;
    }

    .dropdown-menu {
        background-color: #2980B9;
        min-width: 0rem;
    }

    body {
        background-color: #A3E4D7;
        padding-top: 0px;
    }

</style>

    