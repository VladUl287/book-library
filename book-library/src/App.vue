<template>
  <div class="app">
    <NavBar />
    <div class="body-wrap">
      <router-view />
    </div>
  </div>
</template>

<script setup lang="ts">
import NavBar from "@/components/NavBar.vue";

</script>

<style>
#app {
  /* font-family: Avenir, Helvetica, Arial, sans-serif; */
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #f1f1f1;
  height: 100vh;
  overflow: hidden;
  text-align: center;
  box-sizing: border-box;
  background-color: #212529;
}

.app {
  height: 100%;
  display: grid;
  grid-template-rows: auto 1fr;
}

.body-wrap {
  width: 100%;
  height: 100%;
  overflow-y: auto;
}

::-webkit-scrollbar {
  width: 5px;
}

::-webkit-scrollbar-track {
  background-color: transparent;
}

::-webkit-scrollbar-thumb {
  border-radius: 2px;
  background-color: gray;
}
</style>