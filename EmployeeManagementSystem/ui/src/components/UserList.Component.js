import React, { useState, useEffect,useContext } from 'react';
import { Table,Button } from 'react-bootstrap';
import {getUserList,deleteData} from '../service/userService'
import AddUserModal from '../components/CreateUser.Component'
import{BsFillPencilFill,BsFillTrashFill,BsInfoLg}from 'react-icons/bs'
import { UserContext } from '../contexts/UserContext';
import EditUserModal from './EditUser.Component';
import UserDetailModal from './UserInfo.Component'
import swal from "sweetalert";
export const UserList = () => {

    
    const [showModal, setShowModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDetailModal, setShowDetailModal] = useState(false);
    const [selectedUserId, setSelectedUserId] = useState(0);
    const{userList,setUserlist,setUser} = useContext(UserContext);
    const[isEdit,setIsEdit]=useState(false);
    const[isUserDetail,setIsUserDetail]=useState(false);
    // Fetch data from API
    useEffect(() => {
        fetchUserList();
    }, []);

    const fetchUserList = async () => {
       
        const Data = await getUserList();
        console.log("Data",Data);
        Data.map(val=>{
          if(val.passportExpireDate !==null && val.passportExpireDate !==""){
            val.passportExpireDate=val.passportExpireDate.replaceAll('/','-');
            let d = val.passportExpireDate.split('-');
            val.passportExpireDate=`${d[2]}-${d[1]}-${d[0]}`
          }
        }
            )
          setUserlist(Data);
      };

      const handleAddUserClick = () => {
        setIsUserDetail(false);
        setShowDetailModal(false);
        setIsEdit(false);
        setShowModal(true);
      };

      const handleEditClick = (user) => {
        setUser({...user})
            setIsEdit(true);
            setShowEditModal(true);
      };
      const handleDetaliClick = (user) => {
        setUser({...user})
            setIsUserDetail(true);
            setShowDetailModal(true);
      };
      const handleDeleteClick = (id) => {

        swal({
          title: "Are you sure?",
          text: "You will not be able to recover data!",
          icon: "warning",
          buttons: true,
          dangerMode: true,
        })
        .then((willDelete) => {
          if (willDelete) {
            deleteData(id,fetchUserList);
          } 
        });
      };

   
    return (
      <div className='container'>
      
        <section className='py-5' style={{margin:"5px"}}><h1 id="tableLabel">User List <div style={{float:"right",margin:"5px"}}>
            <Button variant="primary" onClick={handleAddUserClick}>Add User</Button></div></h1>

            {isUserDetail?<UserDetailModal showdetailModal={showDetailModal} setDetailsShowModal={setShowDetailModal}/>:""}

        {isEdit? <EditUserModal showModal={showEditModal} setShowModal={setShowEditModal} refreshTable={()=>fetchUserList()} ></EditUserModal>
        :
        <AddUserModal showModal={showModal} setShowModal={setShowModal} refreshTable={()=>fetchUserList()} />}
        
        <Table className='border'>
            <thead>
            <tr>
              <th>Name</th>
              <th>MobileNo</th>
              <th>Email Id</th>
              <th>Employee Type</th>
              <th>Designation</th>
              <th>LocationName</th>
              <th>Nationality</th>
              <th>Actions
                <p style={{width:"150px",margin:"0px"}}></p>
              </th>
            </tr>
          </thead>
            
            <tbody>
              {userList?.length > 0 ? userList.map(user =>
                <tr key={user.id}>
                    <td>{user.name}</td>
                  <td>{user.mobileNo}</td>
                  <td>{user.email}</td>
                  <td>{user.employeeType}</td>
                  <td>{user.designation}</td>
                  <td>{user.locationName}</td>
                  <td>{user.nationality}</td>
                  
                  <td>
                  <Button className='mx-1' onClick={()=>handleDetaliClick({...user})}>
                <BsInfoLg />
                </Button>
                  <Button className='mx-1' variant="primary" onClick={() => handleEditClick({...user})}>
                  <BsFillPencilFill />
                </Button>
                <Button variant="danger" onClick={()=>handleDeleteClick(user.id)}>
                  <BsFillTrashFill />
                </Button>
                
              </td>
                </tr>
              )
              : <tr><td colSpan={8} style={{textAlign:"center"}}>No Data Found</td></tr>
              }
            </tbody>
            </Table>
            <nav>
       
      </nav>
      </section>
      </div>
      
    );
  }
 export default UserList;