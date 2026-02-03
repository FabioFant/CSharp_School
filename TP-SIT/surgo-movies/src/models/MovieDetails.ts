import type Movie from "./Movie";

export default interface MovieDetails extends Movie {
    vote_count: number,
    release_date: string,
    original_language: string,
    runtime: number,
    genres: string[],
    status: string,
    backdrop_path?: string,
}