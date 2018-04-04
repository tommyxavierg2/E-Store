<template>
    <div id="app">

        <b-navbar type="dark" class="space">

            <b-navbar-brand>
                <router-link to="/" style="padding-left: 5%;">
                    <img src="https://firebasestorage.googleapis.com/v0/b/starbase-6d93c.appspot.com/o/productImages%2FE-Store_Logo.PNG?alt=media&token=804da850-b2f9-4ea0-92c2-9a9a101a806d" alt="E-Store logo" width="100px" height="40px">
                    E-Store
                </router-link>
            </b-navbar-brand>

            <b-navbar-nav v-if="checkUserLogged.loggedIn">
                <router-link :to="{ name: 'profile', params: { id: checkUserLogged.user.id }}">Profile </router-link>
            </b-navbar-nav>

            <b-navbar-nav class="ml-auto">

                <b-nav-form>
                    <multiselect v-model="records.multiselect.searchValue" :options="records.multiselect.searchedProduct" :searchable="true" placeholder="¿What you looking for?" label="name" track-by="name" :loading="records.isLoading" :limit="5" @search-change="searchProduct" :options-limit="20" :option-height="100" @select="goToProductDetails" open-direction="bottom">
                        <template slot="option" slot-scope="props" style="background-color: black'"> 
                            <img class="option__image" :src="props.option.image" :alt="props.option.name" style="width: 80px;"> 
                                <div class="option__desc"> 
                                    <span> <router-link class="option__title" :to="{ path: `/product/${props.option.id}`}">{{ props.option.name }}</router-link> </span> 
                                </div> 
                            </template>   
                        <span slot="noResult">Oops! No elements found.</span>
                    </multiselect>
                </b-nav-form>
                
            </b-navbar-nav>

            <b-nav-item-dropdown right>
                <template slot="button-content">
                    <em>Menu</em>
                </template>
                <router-link to="/settings" v-if="checkUserLogged.loggedIn"><icon icon="wrench"/>Settings</router-link>
                <b-dropdown-item to="/login" v-if="!checkUserLogged.loggedIn">Login</b-dropdown-item>
                <b-dropdown-item to="/register" v-if="!checkUserLogged.loggedIn">Register</b-dropdown-item>
                <b-dropdown-item @click="logout()" v-if="checkUserLogged.loggedIn">Logout</b-dropdown-item>
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
                this.records.multiselect.isLoading = true;
                this.$http.get(`${api.url}product/${query}`)
                    .then(response => {
                        this.records.multiselect.searchedProduct = response.data;
                        this.records.isLoading = false;
                    })
                    .catch(err => toastr.warning(err));
            },

            goToProductDetails(selected) {
                this.$router.push(`/detail/${selected.id}`);
            },

            logout() {
                if (confirm('¿Are you sure about logging out?')) {
                    this.$store.commit('logout');
                    this.$router.replace('/');
                    toastr.success('See you soon');
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

    