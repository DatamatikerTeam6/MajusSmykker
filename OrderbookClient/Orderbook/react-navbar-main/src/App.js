import Navbar from "./Navbar"
import Pricing from "./pages/Customer"
import Home from "./pages/Home"
import About from "./pages/Order"
import Login from "./pages/Login"
import Review from "./pages/Review"
import Header from "./Navbar"
import { Route, Routes } from "react-router-dom"


function App() {
  return (
    <>
      <Header />
      <div className="container">
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/Customer" element={<Pricing />} />
          <Route path="/Order" element={<About />} />
          <Route path = "/Review" element={<Review />} />
        </Routes>
      </div>
    </>
  )
}

export default App
