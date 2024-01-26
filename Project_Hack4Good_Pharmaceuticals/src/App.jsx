import {Routes, Route } from 'react-router-dom'
import Main from './components/Main.jsx'
import Search from './components/SearchPage/Search.jsx'
import Header from './components/Header.jsx'
import Footer from './components/Footer.jsx'
import Admin from './components/AdminPanel/Admin.jsx'
import Login from './components/LoginRegistration/Login.jsx'
import Register from './components/LoginRegistration/Register.jsx'
import PageNotFound from './components/404page.jsx'
import Verify from './components/LoginRegistration/VerifyEmail.jsx'
import './App.css'

function App() {
  
  return (
    <>
    <div className='box'>
      <Header />
      <Routes>
        <Route path='/' element={<Main />}/>
        <Route path='/search' element={<Search />}/>
        <Route path='/admin' element={<Admin/>}/>
        <Route path='/login' element={<Login/>}/>
        <Route path='/register' element={<Register/>}/>
        <Route path='/verifyEmail' element={<Verify/>}/>
        <Route path="*" element={<PageNotFound />} />
      </Routes>
      <Footer/>
    </div>
    </>
  )
}

export default App
