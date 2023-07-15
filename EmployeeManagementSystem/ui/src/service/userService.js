
import swal from "sweetalert";
const endpoints = {
  "GetAllUsers": "UserDetails/GetAllUsers",
  "CreateUser": "UserDetails/CreateUser",
  "EditUser": `UserDetails/EditUser`,
  "GetUser": (id) => `UserDetails/GetUser/${id}`,
  "DeleteUser": (id) => `UserDetails/DeleteUser/${id}`,
  "GetAllLocations": "UserDetails/GetAllLocations",
}
export const serverUrl = "http://localhost:52463/api/";



const handleResponse = (response) => {
  if (!response.ok) {
    // throw new Error(response.statusText);
  }
  return response.json();
};

// Function to handle API error
const handleError = (error) => {
  console.error('API Error:', error);

};

// Fetch GET request
export const getUserList = async () => {
  return await fetch(serverUrl + endpoints.GetAllUsers, {
    method: 'GET', // or 'POST', 'PUT', etc.
    headers: {
      'Content-Type': 'application/json',
    },
  })
    .then(handleResponse)
    .catch(handleError);
};

// Fetch POST request
export const createData = async (data, handleClose, setError,clearErrors,refreshUserTable) => {
  return await fetch(serverUrl + endpoints.CreateUser, {
    method: 'Post',
    mode: "cors",
    headers: {
    },
    body: data
  })
    .then((response) => {
      if (!response.ok){
        return  response.json();
      }
      return response
    })
    .then((res) => {
      // Handle the response from the API
      if (!res.ok) {
        // throw new Error(response.statusText);
        if (typeof res === 'object') {
          if (Array.isArray(res)) {
            res.map(val => {
              if (val.propertyName !== 'null') {
                setError(convertToCamel(val["propertyName"]), {
                  type: 'server',
                  message: val["errorMessage"],
                });
              }
              else{
                swal(val.errorMessage, '', 'error');
              }
            })

          }
          else {
            if (res.isduplicate) {
              setError("name", {
                type: 'server',
                message: res["message"],
              });

            }
          }
        }
        clearErrors();
      }
      else {
        swal("User Created Successfully.", '', 'success');
        refreshUserTable();
        handleClose();
      }

    })
    .catch((error) => {
      console.error(error);
    });
};

// Fetch Get User Data request
export const getUser = async (id) => {
  return await fetch(serverUrl + endpoints.getData, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json'
    },
  })
    .then(handleResponse)
    .catch(handleError);
};

// Fetch PUT request
export const updateData = ( data, handleClose, setError,clearErrors,refreshUserTable) => {
  return fetch(serverUrl + endpoints.EditUser, {
    method: 'PUT',
    headers: {
      
    },
    body: data
  })
  .then((response) => {
    if (!response.ok){
      return  response.json();
    }
    return response
  })
  .then((res) => {
    // Handle the response from the API
    if (!res.ok) {
      // throw new Error(response.statusText);
      if (typeof res === 'object') {
        if (Array.isArray(res)) {
          res.map(val => {
            if (val.propertyName !== 'null') {
              setError(convertToCamel(val["propertyName"]), {
                type: 'server',
                message: val["errorMessage"],
              });
            }
            else{
              swal(val.errorMessage, '', 'error');
            }
          })

        }
        else {
          if (res.isduplicate) {
            setError("name", {
              type: 'server',
              message: res["message"],
            });

          }
        }
      }
      clearErrors();
    }
    else {
      swal("User Updated Successfully.", '', 'success');
      refreshUserTable();
      handleClose();
    }

  })
  .catch((error) => {
    console.error(error);
  });
};

// Fetch DELETE request
export const deleteData = (id,fetchUserList) => {
  console.log("serverUrl + endpoints.DeleteUser(id)",serverUrl + endpoints.DeleteUser(id))
  return fetch(serverUrl + endpoints.DeleteUser(id), {
    method: 'DELETE'
  }) .then((response) => {
    if (response.ok) {
      // Handle successful deletion
      fetchUserList();
      swal("User Deleted successfully.", '', 'success');
    } else {
      
    }
  })
  .catch((error) => {
    
  });
};

// Fetch GET request for Location List
export const getLocationList = async () => {
  return await fetch(serverUrl + endpoints.GetAllLocations, {
    method: 'GET', // or 'POST', 'PUT', etc.
    headers: {
      'Content-Type': 'application/json',
    },
  })
    .then(handleResponse)
    .catch(handleError);
};

function convertToCamel(str) {
  return str.charAt(0).toLowerCase() + str.slice(1);
}