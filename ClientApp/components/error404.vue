<template>
  <div>
      <div class="jumbotron">
        <h1>{{page.title}}</h1>
      </div>
      <div class="container">
        <img :src="page.image" alt="not found" style="height: 100%; width: 100%;">
        <p>{{page.suggestion}}</p>
        <router-link :to="page.link">See more</router-link>
      </div>
  </div>
</template>

<script>
    export default {
        data() {
            return {
                page: { title: 'Error 404, not found', image: 'https://firebasestorage.googleapis.com/v0/b/morse-code-7eb28.appspot.com/o/notfound.jpg?alt=media&token=fa3382ca-4d51-4fc3-b952-a30851ec5eb1', suggestion: '¿Would you like to see more?', link: '/'}
            }
        }
    }
    
</script>