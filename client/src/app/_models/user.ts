export interface User {
  // razlicit je od interfejsa u c#, kaze se da je interfejs tip od nekog tipa
  username: string;
  token: string;
}

/*
let data: number | string = 42;

data = "10";

interface Car {
    color: string;
    model: string;
    topSpeed?: number
}


const car1 : Car = {
    color: 'blue',
    model: 'BMW',
}

const car2 : Car = {
    color: 'red',
    model: 'Mercedes',
    topSpeed: 100
}

const multiplay = (x: number, y: number) : void => {
     x*y;
}
*/
