const baseURL = "http://localhost:5288/api/movie";

const  movieFetch = async () => {
    const res = await fetch(baseURL);
    const data = await res.json();
    console.log(data);
    return data;
}

movieFetch();

//This object becomes the JSON body. Its shape matches CreateMovieDto
const movie = {
    title: "2Fast 2Furious",
    director: "Callen Thomason",
    releaseYear: 2001,
    rating: 100
}



const createMovie = async (newMovie) => {
    const res = await fetch(`${baseURL}/addmovie`, {
        method: "POST",
        //Tells the API the body is JSON
        headers: { "Content-Type": "application/json"},
        body: JSON.stringify(newMovie)
    });
    const data = await res.json();
    console.log(data);
}

//adds the movie to our list using our movie object "2fast 2Furious".
//createMovie(movie);