import { Link, useMatch, useResolvedPath } from "react-router-dom"


export default function Navbar() {
  return (
    <nav className="nav">
      <div className="logo-container">
      <img src="./Images/MajusSmykker_Logo.png" alt="MajusSmykker Logo" height="50" width="50"/>     
      <Link to="/" className="site-title">
          Guldsmed Sabine Majus - Ordrebog
        </Link>
      </div>
      <ul>
        <CustomLink to="/order">Ordre</CustomLink>
        <CustomLink to="/customer">Opret Kunde</CustomLink>
        <CustomLink to="/login">Login</CustomLink>
      </ul>
    </nav>
  )
}

function CustomLink({ to, children, ...props }) {
  const resolvedPath = useResolvedPath(to)
  const isActive = useMatch({ path: resolvedPath.pathname, end: true })

  return (
    <li className={isActive ? "active" : ""}>
      <Link to={to} {...props}>
        {children}
      </Link>
    </li>
  )
}
