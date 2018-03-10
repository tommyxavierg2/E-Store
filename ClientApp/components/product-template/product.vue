<template>
    <div class="card text-xs-center">
        
        <ul class="list-group list-group-flush">
            
            <li class="list-group-item"> 
                <slot></slot>
            </li>
    
        </ul>

        <router-link class="card-title" :to="`/detail/${product.id}`">
            <img :src="product.image" :alt="product.name" class="card-img-top">
        </router-link>
    
        <div class="card-block">
    
            <router-link class="card-title" :to="`/detail/${product.id}`">{{product.name}}</router-link> <br>

            <router-link class="card-text" :to="`/detail/${product.id}`">{{product.description}}</router-link>
    
            <p class="card-text">{{product.price}}</p>
    
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
