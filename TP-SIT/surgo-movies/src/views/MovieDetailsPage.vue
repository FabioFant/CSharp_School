<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { MovieService } from '../services/MovieService';
import type MovieDetails from '../models/MovieDetails';
import type CrewMembers from '../models/CrewMembers';

const route = useRoute();
const service = new MovieService();
const movie = ref<MovieDetails | null>(null);
const crew = ref<CrewMembers | null>(null);
const director = ref<string>('');
const writer = ref<string>('');

onMounted(async () => {
    const id = route.params.id as string;
    
    try {
        movie.value = await service.getMovieDetails(id);
        crew.value = await service.getMovieCrew(id);
        
        if (crew.value) {
            const directorMember = crew.value.crew.find(m => m.job === 'Director');
            const writerMember = crew.value.crew.find(m => m.job === 'Writer' || m.job === 'Screenplay');
            director.value = directorMember?.name || 'N/A';
            writer.value = writerMember?.name || 'N/A';
        }
    } catch (e) {
        console.log("Error:", e);
    }
});

function formatRuntime(minutes: number): string {
    const hours = Math.floor(minutes / 60);
    const mins = minutes % 60;
    return `${hours}h ${mins}m`;
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
    <div v-if="movie">
        <div class="position-relative shadow-lg" style="height: 350px;">
            <img 
                v-if="movie.backdrop_path" 
                :src="movie.backdrop_path" 
                :alt="movie.title"
                class="w-100 h-100"
                style="object-fit: cover;"
            >
            <div v-else class="w-100 h-100 bg-dark"></div>
            <div 
                class="position-absolute w-100 h-100 d-flex flex-column justify-content-center align-items-center"
                style="top: 0; left: 0; background: #C7B29999;"
            >
                <h1 class="mb-2 text-dark font-weight-bold shadow-for-text">{{ movie.title }}</h1>
                <h2 class="shadow-for-text">
                    <span class="star-rating">{{ getStars(movie.stars) }}</span>
                    <span class="text-dark">({{ movie.vote_count }})</span>
                </h2>
            </div>
            <div class="vignette-overlay"></div>
        </div>
        
        <div class="container py-4 mt-5 mb-5">
            <h5 class="text-left mb-3">
                <span class="mr-3">{{ movie.release_date }}</span>
                <span class="mr-3">{{ movie.original_language.toUpperCase() }}</span>
                <span>{{ formatRuntime(movie.runtime) }}</span>
            </h5>
            
            <h4 class="font-weight-bold mb-3">{{ movie.genres.join(', ') }}</h4>
            
            <p class="mb-3">{{ movie.overview }}</p>
            
            <p class="mb-3">
                <span class="font-weight-bold text-success">{{ movie.status }}</span>
            </p>
            
            <p class="mb-0">
                <strong>{{ director }}</strong> - Director, Writer
            </p>
        </div>
    </div>
    
    <div v-else class="container py-5 text-center">
        <div class="spinner-border text-secondary" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
</template>

<style scoped>
.vignette-overlay {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: radial-gradient(transparent 50%, #C7B299 100%);
    pointer-events: none;
}
</style>
