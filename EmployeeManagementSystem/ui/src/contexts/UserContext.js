import {useState}  from 'react';
import React from 'react';
// Create the context
export const UserContext = React.createContext({
    userList:[],
    user:{}
});

export const UserProvider =({children})=>{
    const [userList,setUserlist]=useState();
    const [user,setUser]=useState();
    return(
        <UserContext.Provider value={{userList,setUserlist,userData:user,setUser}}  >
            {children}
        </UserContext.Provider>
    )
}