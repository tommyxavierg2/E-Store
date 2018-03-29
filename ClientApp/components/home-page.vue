<template>
    <div>
    
        <h1 class="jumbotron">{{records.page.header}}</h1>

        <div class="row">
            <div class="col-md-6">
                <h2>{{records.page.title}}</h2> 
            </div>

            <div class="col-md-6">
                <button v-if="checkUserLogged.user.Client" type="button" class="btn" style="float: right; position: static;">Add To Cart {{checkUserLogged.cart.length}}</button>
            </div>
        </div>
    
        <div class="row">

            <div class="col-md-4" v-for="product in records.products" :key="product.id" style="margin-bottom: 10px;">
    
                <product :product="product">
                    <button class="btn btn-info btn-block" v-if="checkUserLogged.user.Client" @click="addToCart(product)"><icon icon="cart-plus"/> </button>
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

            addToCart(item) {
                if (confirm(`¿Are you sure about adding item ${item.name} to your cart?`)) {
                    item.buyerUserId = this.$store.state.user.id;
                    this.$http.post(`${api.url}cart`, item)
                        .then(res => alert(`Item ${item.name} has been added sucessfully to your cart`))
                        .catch(err => alert(err));
                }
            }
        },
    
        created() {
            this.init();
        }   
    
    }
</script>

<style>
    
</style>
