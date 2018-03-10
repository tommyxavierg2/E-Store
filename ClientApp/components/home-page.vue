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
    
            <div class="col-md-3" v-for="product in records.products" :key="product.id">
    
                <product :product="product">
                    <button class="btn btn-info btn-block"><icon icon="cart-plus"/> </button>
                </product>
    
            </div>
    
        </div>
    
    </div>
</template>

<script>
    import product from './product-template/product';
    import {api} from '../store/api';
    
    export default {
        data() {
            return {
                records: {
                    page: { header: '', title: '' },
                    products: []
                }
            }
        },
    
        components: {
            'product': product
        },

        computed: {},
    
        methods: {
            init() {
                this.records = { 
                    page: { 
                        header: 'Home', title: 'Recently added'
                    }, 
                    multiselect: {
                        isLoading: false, searchValue: '', searchedProduct: [] 
                    },
                    products: [] 
                    };
                this.getProducts();
            },

            getProducts() {
                this.$http.get(`${api.url}products?_limit=5`)
                .then( response => this.records.products = response.data)
                .catch( err => toastr.warning(err));
            }
        },
    
        created() {
            this.init();
        }   
    
    }
</script>

<style>
    
</style>
