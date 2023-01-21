import "bootstrap/dist/css/bootstrap.min.css";
import { Route, Routes } from "react-router-dom";
import Home from "./pages/home/Home";
import Books from "./pages/books/Books";
import Authors from "./pages/authors/Authors";
import Navbar from "./components/Navbar";
import BookCreate from "./pages/books/BookCreate";
import { toast, ToastContainer } from "react-toastify";
import BookEdit from "./pages/books/BookEdit";
import BookIndex from "./pages/books/BookIndex";
import AuthorEdit from "./pages/authors/AuthorEdit";
import AuthorCreate from "./pages/authors/AuthorCreate ";
import AuthorIndex from "./pages/authors/AuthorIndex";

export default function App() {
  return (
    <div>
      <Navbar/>
      <Routes>
        {/* Books  */}
        <Route path='/' element={<Home/>} />
        <Route path='/books' element={<Books/>} />
        <Route path='/bookIndex' element={<BookIndex/>} />
        <Route path='/createBook' element={<BookCreate/>} />
        <Route path='/editBook/:id' element={<BookEdit/>} />
        {/* Authors */}
        <Route path='/authors' element={<Authors/>} />
        <Route path='/editAuthor/:id' element={<AuthorEdit/>} />
        <Route path='/createAuthor' element={<AuthorCreate/>} />
        <Route path='/authorIndex' element={<AuthorIndex/>} />
      </Routes>
      <ToastContainer position={toast.POSITION.TOP_RIGHT} autoClose={3000} />
    </div>
  );
}
