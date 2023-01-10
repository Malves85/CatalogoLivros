import { Routes, Route } from 'react-router-dom';
import Home from './pages/home';
import Books from './pages/books';
import Authors from './pages/authors';
const Main = () => {
return (         
    <Routes>
    <Route path='/' element={<Home/>} />
    <Route path='/books' element={<Books/>} />
    <Route path='/authors' element={<Authors/>} />
  </Routes>
);
}
export default Main;