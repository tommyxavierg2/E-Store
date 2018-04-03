<template>
    <div class="card text-xs-center">

        <div class="row">
            <div class="col-md-6">
                <div class="card text-xs-center">
                    <router-link :to="{ path: `/detail/${product.id}`}">
                        <img :src="product.image" :alt="product.name" class="card-img-top">
                    </router-link>
                    <div>
                        <router-link :to="{ path: `/detail/${product.id}` }" style="padding-left: 10%;">{{product.name}}</router-link> <br>
                    </div>
                </div>
            </div>
            <div class="col-md-6"> 
                <div v-if="icon" class="close">
                    <slot></slot>
                </div> 
                <div class="card-block">
                    <router-link class="card-text" :to="{ path: `/detail/${product.id}` }">{{product.description}}</router-link>
                    <h4 class="card-text">RD$ {{product.price}}</h4>
                </div>
            </div>
        </div> 

        <div v-if="!icon" class="list-group list-group-flush">
            <slot></slot>
        </div> 
     
    </div>
</template>

<script>
    export default {
    
        data() {
    
            return {}
    
        },
    
        props: ['product', 'icon']
    
    }
</script>
<style scoped>

</style>

