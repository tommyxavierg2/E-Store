<template>
    <div class="card text-xs-center pad" style="width: 80%;">

    <div class="row">
        <div class="col-md-6">
            <router-link class="card-title" :to="`/detail/${order.product.id}`">
                <img :src="order.product.image" :alt="order.product.name" class="card-img-top" width="50%" height="80%"> 
            </router-link>
        </div>
        <div class="col-md-6">
                
            <div class="card-block">
        
                <router-link class="card-title" :to="`/detail/${order.product.id}`">{{order.product.name}}</router-link> <br>

                <p class="card-text">{{order.product.description}}</p>
        
                <p class="card-text">RD$ {{order.product.price}}</p>

                <p class="card-text">{{order.date}}</p>
        
            </div>
        </div>
    </div>  
</div>
</template>
<script>
    export default {
        data() {
            return {
                
            }
        },
        props: [ 'order' ]
    }
</script>

<style>
    .pad {
        padding: 15px;
        margin-bottom: 15px;
        margin: 15px;
    }
</style>
