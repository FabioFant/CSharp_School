<script setup lang="ts">
import { RouterLink } from 'vue-router';
</script>

<template>
    <header class="py-2 sticky-top" style="background-color: #d9d9d9;">
        <div class="container p-4">
            <RouterLink to="/">
                <img src="/icon-brand.png" alt="Surgo Movies" style="height: 32px;">
            </RouterLink>
        </div>
    </header>
</template>

<style scoped>
</style>
