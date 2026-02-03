import type Movie from "../models/Movie";
import type MovieDetails from "../models/MovieDetails";
import type CrewMembers from "../models/CrewMembers";
import { enviroment } from "../enviroment/enviroment";

const IMAGE_BASE_URL = "https://image.tmdb.org/t/p/w500";

export class MovieService {

    private async getApiData(endpoint: string, queryParams: string = ""): Promise<any> {
        try {
            const url = `${enviroment.apiUrl}${endpoint}?api_key=${enviroment.apiKey}${queryParams}`;

            const res = await fetch(url);

            if (!res.ok) {
                throw new Error(`Error fetching data from ${endpoint}`);
            }

            return await res.json();
        } catch (error) {
            console.error(error);
            throw error;
        }
    }

    async getTrendingMovies(): Promise<Movie[]> {
        const data = await this.getApiData("/trending/movie/week");

        return data.results.map((item: any) => ({
            title: item.title,
            overview: item.overview,
            image: `${IMAGE_BASE_URL}${item.poster_path}`,
            stars: parseFloat((item.vote_average / 2).toFixed(1)),
            id: item.id
        }));
    }

    async searchMovie(query: string): Promise<Movie[]> {
        const data = await this.getApiData("/search/movie", `&query=${query}`);

        return data.results.map((item: any) => ({
            title: item.title,
            overview: item.overview,
            image: `${IMAGE_BASE_URL}${item.poster_path}`,
            stars: parseFloat((item.vote_average / 2).toFixed(1)),
            id: item.id
        }));
    }

    async getMovieDetails(id: string): Promise<MovieDetails> {
        const data = await this.getApiData(`/movie/${id}`);

        return {
            title: data.title,
            overview: data.overview,
            image: `${IMAGE_BASE_URL}${data.poster_path}`,
            stars: parseFloat((data.vote_average / 2).toFixed(1)),
            id: data.id,
            vote_count: data.vote_count,
            release_date: data.release_date,
            original_language: data.original_language,
            runtime: data.runtime,
            status: data.status,
            genres: data.genres.map((g: any) => g.name),
            backdrop_path: data.backdrop_path ? `https://image.tmdb.org/t/p/original${data.backdrop_path}` : undefined
        };
    }

    async getMovieCrew(movieId: string): Promise<CrewMembers> {
        const data = await this.getApiData(`/movie/${movieId}/credits`);

        return {
            crew: data.crew.map((member: any) => ({
                name: member.name,
                job: member.job
            }))
        };
    }
}