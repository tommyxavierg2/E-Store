<template>
    <div>
    
        <h1 class="jumbotron">{{records.page.header}}</h1>

        <div class="row">
            <div class="col-lg-6">
                <h2>{{records.page.title}}</h2> 
            </div>

            <div class="col-lg-6">

            </div>
        </div>
    
        <div class="row">
    
            <div class="col-md-4" v-for="product in records.products" :key="product.id" style="margin-bottom: 10px;">
    
                <product :product="product">
                    <button class="btn btn-info btn-block" v-if="$store.state.user.Client" @click="addToCart(product)"><icon icon="cart-plus"/> </button>
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

        computed: {},
    
        methods: {
            init() {
                this.getProducts();
            },

            getProducts() {
                this.$http.get(`${api.url}inventories?state=1&_limit=25`)
                .then( response => this.records.products = response.data)
                .catch( err => toastr.warning(err));
            },

            addToCart(item) {
                if (confirm(`¿Are you sure about adding item ${item.name} to your cart?`)) {
                    item.buyerUserId = this.$store.state.user.id;
                    this.$http.post(`${api.url}carts`, item)
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
