<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { MovieService } from '../services/MovieService';
import type Movie from '../models/Movie';
import HeroBanner from '../components/HeroBanner.vue';
import MovieCard from '../components/MovieCard.vue';

const service = new MovieService();
const movies = ref<Movie[]>([]);
const searchQuery = ref('');
const isSearching = ref(false);

onMounted(async () => {
    await loadTrendingMovies();
});

async function loadTrendingMovies() {
    try {
        movies.value = await service.getTrendingMovies();
        isSearching.value = false;
    } catch (e) {
        console.log("Error:", e);
    }
}

async function searchMovies() {
    if (!searchQuery.value.trim()) {
        await loadTrendingMovies();
        return;
    }
    
    try {
        movies.value = await service.searchMovie(searchQuery.value);
        isSearching.value = true;
    } catch (e) {
        console.log("Error:", e);
    }
}
</script>

<template>
    <div style="background-color: #D9D9D9;">
        <HeroBanner />
        
        <div class="container py-4">
            <div class="d-flex justify-content-between align-items-center mb-4">
                <h3 class="mb-0 font-weight-bold shadow-for-text">{{ isSearching ? 'Search Results' : 'Trending Movies' }}</h3>
                <div class="w-25">
                    <h4 class="text-right shadow-for-text">Search a film</h4>
                    <input 
                    type="text" 
                    class="form-control border-0 shadow-lg text-right"
                    v-model="searchQuery"
                    @keyup.enter="searchMovies"
                    style="background-color: #C7B299; border-radius: 8px; padding: 8px 16px;"
                    />
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-4 mb-4" v-for="movie in movies" :key="movie.id">
                    <MovieCard :movie="movie" />
                </div>
            </div>
            
            <p v-if="movies.length === 0" class="text-center text-muted mt-4">
                No movies found.
            </p>
        </div>
    </div>
</template>

<style scoped>
</style>
