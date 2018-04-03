<template>
    <div class="card text-xs-center">

        <div class="row card-title">
            <div class="col-md-6">
                <h4 class="card-text">{{orders.date}}</h4>
            </div>
            <div class="col-md-6">
                <h4 class="card-text">{{orders.id}}</h4>
                <p class="card-text">{{orderState}}</p>
            </div>
        </div>

        <div class="row card" v-for="order in orders.product" :key="order.id">
            <div class="col-md-6">
                <router-link class="card-title" :to="`/detail/${order.id}`">
                    <img :src="order.image" :alt="order.name" class="card-img-top" width="50%" height="80%"> 
                </router-link>
            </div>
            <div class="col-md-6">
                    
                <div class="card-block">
            
                    <router-link class="card-title" :to="`/detail/${order.id}`">{{order.name}}</router-link> <br>

                    <p class="card-text">{{order.description}}</p>
            
                    <p class="card-text">RD$ {{order.price}}</p>
                </div>
            </div>
        </div> <br>  
        <div> <slot></slot> </div> <br>

    </div>
</template>
<script>
    export default {
        data() {
            return {
                
            }
        },
        computed: {
            orderState() {
                if (this.orders.state === 0) 
                    return "Cancelled";
                else if (this.orders.state === 1) 
                    return "In progress";
                else if (this.orders.state === 2) 
                    return "Sent";
                else {
                    return "Received";
                }
            }
        },
        props: [ 'orders' ]
    }
</script>

<style>

</style>
