const baseURL = "http://localhost:5288/api/movie";

const status = document.getElementById("status");
const list = document.getElementById("movies");
const form = document.getElementById("movie-form");
const title = document.getElementById("title");
const director = document.getElementById("director");
const releaseYear = document.getElementById("year");
const rating = document.getElementById("rating");

//Arrow Functions do NOT need to be async
// Building an <li> for each movie on our list
const showMovies = (movies) => {
       //Clears out the old list first, so movies dont show up twice.
       list.textContent = "";
       
       //movies is the list
       //movie is the element inside our list
       movies.forEach((movie) => {
           console.log(movie);
           const item = document.createElement("li");
           item.classList.add("movie");

           const titleHeader = document.createElement("h2");
           titleHeader.textContent = `${movie.title} ${movie.releaseYear}`;

           const details = document.createElement("p");
           details.textContent = `${movie.director}, rating ${movie.rating}/10`;

           item.appendChild(titleHeader);
           item.appendChild(details);

           list.appendChild(item);
       });
}

form.addEventListener("submit", async (event) => {
      event.preventDefault() // stops the page from reloading

      //This object becomes JSON body. Its shape matches our CreateMovieDto
      const movie = {
    title: title.value,
    director: director.value,
    releaseYear: releaseYear.value,
    rating: rating.value
}

await createMovie(movie);
//Emptys the form and reloads the list, so the new movies show up
form.reset();
await movieFetch();

});

const  movieFetch = async () => {
    // Try / Catch: if anything inside "try" fails, we jump to "catch" instead of crashing
    try{
    const res = await fetch(baseURL);
    //we check response.ok ourselves and throw error if the status isn't 200
    if(!res.ok){
      throw new Error(`Request failed: ${res.status}`);
    }
    const data = await res.json();
    //data is a list of movies
    showMovies(data);
    status.textContent = "";
    } catch (error) {
        status.textContent = "Couldn't load movies. Check the console."
        console.log(error);
    }
}

movieFetch();

//This object becomes the JSON body. Its shape matches CreateMovieDto
// const movie = {
//     title: "2Fast 2Furious",
//     director: "Callen Thomason",
//     releaseYear: 2001,
//     rating: 100
// }



const createMovie = async (newMovie) => {
    const res = await fetch(`${baseURL}/addmovie`, {
        method: "POST",
        //Tells the API the body is JSON
        headers: { "Content-Type": "application/json"},
        body: JSON.stringify(newMovie)
    });
    if(!res.ok) {
        status.textContent = "Movie already exists within the database.";
        throw new error(`res.status`)
    }
    const data = await res.json();
    console.log(data);
}

//adds the movie to our list using our movie object "2fast 2Furious".
//createMovie(movie);