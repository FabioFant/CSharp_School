<script setup lang="ts">
import AppHeader from './components/AppHeader.vue';
import AppFooter from './components/AppFooter.vue';
</script>

<template>
  <div class="d-flex flex-column min-vh-100">
    <AppHeader />
    <main class="flex-grow-1">
      <router-view />
    </main>
    <AppFooter />
  </div>
</template>

<style scoped>
</style>
