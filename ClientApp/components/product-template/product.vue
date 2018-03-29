<template>
    <div class="card text-xs-center">

        <div class="row">
            <div class="col-md-6">
                <router-link class="card-title" :to="{ path: `/detail/${product.id}`}">
                    <img :src="product.image" :alt="product.name" class="card-img-top">
                </router-link>
            </div>
            <div class="col-md-6">
                    
                <div class="card-block">
            
                    <router-link class="card-title" :to="{ path: `/detail/${product.id}` }">{{product.name}}</router-link> <br>

                    <router-link class="card-text" :to="{ path: `/detail/${product.id}` }">{{product.description}}</router-link>
            
                    <p class="card-text">RD$ {{product.price}}</p>
            
                </div>

            </div>

        </div> 

        <ul class="list-group list-group-flush">
            
            <li class="list-group-item"> 
                <slot></slot>
            </li>

        </ul> 
     
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
