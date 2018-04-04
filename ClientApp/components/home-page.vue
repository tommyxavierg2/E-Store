<template>
    <div>
    
        <h1 class="jumbotron">{{records.page.header}}</h1>

        <div class="row">
            <div class="col-md-6">
                <h2>{{records.page.title}}</h2> 
            </div>

            <div class="col-md-6">
                <button v-if="checkUserType" type="button" class="btn" style="float: right; position: static;" @click="addItemsToCart(records.cart)" :disabled="!checkUserLogged.cart.length">Add To Cart {{checkUserLogged.cart.length}}</button>
            </div>
        </div>
    
        <div class="row">

            <div class="col-md-3" v-for="product in records.products" :key="product.id" style="margin-bottom: 10px;">
    
                <product :product="product">
                    <button class="btn btn-info btn-block" v-if="checkUserType" @click="addToCart(product)"><icon icon="cart-plus"/> RD$ {{product.price}} </button>
                </product>
    
            </div>
    
        </div>
    
    </div>
</template>

<script>
    import product from './product-template/product';
    import store from '../store/index';
    import {api} from '../store/api';
    
    export default {
        data() {
            return {
                records: {
                    page: { header: 'Home', title: 'Recently added' },
                    products: [],
                    cart: {
                        id: '', products: [], date: '', state: 1, totalPrice: null, userId: ''
                    },
                    multiselect: {
                        isLoading: false, searchValue: '', searchedProduct: [] 
                    },
                }
            }
        },
    
        components: {
            'product': product
        },

        computed: {
            checkUserLogged() {
                return this.$store.state;
            },

            checkUserType() {
                if (this.checkUserLogged.loggedIn && !this.checkUserLogged.user.accountType)
                    return true;
                else 
                    return false; 
            }
        },
    
        methods: {
            init() {
                this.getProducts();
            },

            getProducts() {
                this.$http.get(`${api.url}product?state=1`)
                .then( response => this.records.products = response.data)
                .catch( err => toastr.warning(err));
            },

            addItemsToCart(recordsCart) {
                if (confirm(`¿Are you sure about adding these items to your cart?`)) {
                    recordsCart.products = this.checkUserLogged.cart;
                    recordsCart.userId = this.checkUserLogged.user.id;
                    recordsCart.date = new Date().toLocaleString();
                    this.checkUserLogged.cart.forEach(item => {
                        recordsCart.totalPrice += item.price;
                    });
                    this.$http.post(`${api.url}cart`, recordsCart)
                        .then(res => {
                                alert(`Items added sucessfully to your cart`);
                                this.$router.push(`/profile/${this.checkUserLogged.user.id}`);
                                this.checkUserLogged.cart = [];
                            })
                        .catch(err => alert(err));
                }
            },

            addToCart(item) {
                this.$store.commit('addItemToCart', item);
            }
        },
    
        created() {
            this.init();
        }   
    
    }
</script>

<style>
    
</style>
