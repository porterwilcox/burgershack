<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <div
    v-for="value in burgers"
    :key="value.Id"
    >
      {{value}}  
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'

export default {
  name: 'home',
  components: {
    HelloWorld
  },
  created(){
    this.$store.dispatch("getAllBurgers");
  },
  computed: {
    burgers(){
      this.$store.state.burgers
    }
  }
}
</script>
