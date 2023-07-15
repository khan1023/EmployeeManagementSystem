import React from 'react'
import Header from '../components/Header.Component'
import { Outlet } from 'react-router-dom'
function LayoutComponent() {
  return (
    <div>
        <Header />
        <Outlet />
    </div>
  )
}

export default LayoutComponent