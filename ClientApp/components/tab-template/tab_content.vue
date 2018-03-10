<template>
    <div :id="name" :class="tab_class" role="tabpanel">
          <product :product="content"></product>
    </div>
</template>

<script> 
  import product from './../product-template/product';        
  
  export default {
    data() {
      return {
      }
    },
    props: ['content', 'name', 'tab_class'],
    components: {
      'product': product
    },
    methods: {

    }
  }
</script>