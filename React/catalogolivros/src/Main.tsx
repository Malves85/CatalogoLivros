import { Routes, Route } from 'react-router-dom';
import Home from './pages/home';
import Books from './pages/books';
const Main = () => {
return (         
    <Routes>
    <Route path='/' element={<Home/>} />
    <Route path='/books' element={<Books/>} />
  </Routes>
);
}
export default Main;