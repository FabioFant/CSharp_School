<script setup lang="ts">
import type Movie from '../models/Movie';
import { useRouter } from 'vue-router';

const props = defineProps<{ 
    movie: Movie
}>();

const router = useRouter();

function goToDetails() {
    router.push(`/movie/${props.movie.id}`);
}

function getStars(rating: number): string {
    const fullStars = Math.floor(rating);
    let stars = '';
    for (let i = 0; i < 5; i++) {
        stars += i < fullStars ? '★' : '☆';
    }
    return stars;
}
</script>

<template>
    <div 
        class="card h-100 border rounded-8 shadow-lg" 
        role="button"
        @click="goToDetails"
    >
        <div class="pl-3 pt-3 w-50">
            <img 
                :src="movie.image" 
                class="img-fluid rounded-3" 
                :alt="movie.title"
            >
        </div>
        <div class="card-body">
            <h3 class="card-title mb-1">{{ movie.title }}</h3>
            <h4 class="text-warning mb-2">{{ getStars(movie.stars) }}</h4>
            
            <p class="card-text small text-muted text-truncate">
                {{ movie.overview }}
            </p>
        </div>
    </div>
</template>
