import * as React from 'react';
import { Routes, Route } from 'react-router-dom';

const Home = React.lazy(() => import('./pages/home'));
const Books = React.lazy(() => import('./pages/books'));
const Loading = () => <p>Loading ...</p>;

const Main2 = () => {
return (         

    <React.Suspense fallback={<Loading />}>
    <Routes>
      <Route path='/' element={<Home/>} />
      <Route path='/books' element={<Books/>} />
    </Routes>
  </React.Suspense>
  
);
}
export default Main2;