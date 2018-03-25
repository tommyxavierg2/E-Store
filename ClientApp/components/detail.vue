<template>
    <div>

        <h1 class="jumbotron">Detail</h1>

        <div style=" width: 100%; height: 100%;">
            <product :product="product"></product>
        </div>

    </div>
  
</template>

<script>
import {api} from '../store/api';
import product from './product-template/product';

export default {
    data() {
        return {
            product: {},
        }
    },

    components: {
        'product': product
    },

    created() {
        this.getProduct(this.$route.params.id);
    },

    methods: {
        getProduct(id) {
            this.$http.get(`${api.url}inventories?id=${id}`)
                .then( response => this.product = response.data[0])
                .catch( err => toastr.error(err));
        }
    }
}
    
</script>