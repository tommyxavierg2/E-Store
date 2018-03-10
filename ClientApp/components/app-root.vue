<template>
    <div id="app" class="container-fluid">

        <nav class="navbar-expand-lg navbar-light bg-dark">

            <div class="collapse navbar-collapse">
                <router-link class="navbar-brand" to="/">E-Store</router-link>

                <ul class="navbar-nav mr-auto mt-2 mt-lg-0" v-if="checkUserLogged.loggedIn">
                    <li class="nav-item">
                        <router-link :to="{ name: 'profile', params: { id: checkUserLogged.user.id }}">Profile</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link :to="{ name: 'cart', params: { id: checkUserLogged.user.id }}">Cart</router-link>
                    </li>
                    <li class="nav-item">
                        <multiselect :v-model="records.multiselect.searchValue" :options="records.multiselect.searchedProduct" :searchable="true" placeholder="¿What you looking for?" label="name" track-by="name" :loading="records.isLoading" :limit="5" @search-change="searchProduct" :options-limit="20" :option-height="100" @select="goToProductDetails" open-direction="bottom">
                            <template slot="option" slot-scope="props"> <img class="option__image" :src="props.option.image" :alt="props.option.name" style="width: 60px">
                                <div class="option__desc">
                                    <span class="option__title">{{ props.option.title }}</span>
                                    <span class="option__small">{{ props.option.desc }}</span>
                                </div>
                            </template> 
                            <span slot="noResult">Oops! No elements found.</span>
                        </multiselect>
                    </li>
                    <li class="nav-item">
                        <button type="button" class="btn-info" @click="logout()">
                            <icon icon="sign-out-alt"/>    
                        </button>
                    </li>
                </ul>

            </div>

        </nav>
        <router-view class="space"></router-view>

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
                return {
                    user: this.$store.state.user,
                    loggedIn: this.$store.state.loggedIn
                };
            }
        },

        methods: {
            searchProduct(query) {
                this.records.multiselect.isLoading = true;
                this.$http.get(`${api.url}products?name=${query}`)
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
                    toastr.success('See you soon');
                    this.$router.replace('/login');
                }
            }
        },

        created() {
            if (!store.state.loggedIn) {
                let user = localStorage.getItem('userData');
                if (user) {
                    this.$store.commit('login', JSON.parse(user));
                } else {
                    this.$router.replace('/login');    
                }
            } 
        }
    }
</script>

<style>
    ul {
        list-style-type: none;
        margin: 0;
        padding: 0;
    }

    .space {
        margin-top: 2%;
        margin-left: 10%;
        margin-right: 10%;
    }
</style>

    