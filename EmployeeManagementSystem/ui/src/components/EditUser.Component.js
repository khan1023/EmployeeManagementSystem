import React, { useState,useEffect,useContext } from 'react';
import { Button, Modal, Form,Row,Col } from 'react-bootstrap';
import {useForm} from 'react-hook-form';
import swal from 'sweetalert';
import {updateData,getLocationList} from '../service/userService';
import rules from '../common/validationRules';
import { UserContext } from '../contexts/UserContext';
import {BsFillFileEarmarkTextFill} from  'react-icons/bs'
const EditUserModal = ({ showModal, setShowModal,refreshTable }) => {
    
const [passportFile,setpassportFile]=useState(null);
const [emirateIdFile,setemirateIdFile]=useState(null);
const [drivingLicenceFile,setdrivingLicenceFile]=useState(null);
const [personPhoto,setpersonPhoto]=useState(null);
const [locations,setlocations]=useState([]);
const {userData}=useContext(UserContext);


const {register,handleSubmit,watch,reset,setValue,setError,clearErrors ,formState:{errors,isValid}} = useForm({mode:"all"});

const nationalityList = ["Albanian","Bahraini","Filipino"];
const Designatios = ["Senior Software Engineer","Software Engineer","System Analyst"];
const employeeTypeList = ["Full-time employees","Part-time employees","Temporary employees"];

useEffect(() => {
    
    fetchLocationList();
    
}, []);
useEffect(() => {
    reset(userData);
    setTimeout(() => {
      setValue('locationId', userData.locationId);
    }, 300);
    
  }, [userData, reset]);


const fetchLocationList = async () => {
    const Data = await getLocationList();
    setlocations(Data);
  };
  
 const onSubmit = (data) => {
  debugger;
  if(!(userData?.passportFile || userData?.emirateIdFile || userData?.drivingLicenceFile))
  {
      swal("File Required","Please Select atleast one file form Passport,Driving Licence or Emirate Id File.","error")
      return false;
  }
if(data.passportNo !="" && (data.passportFile=="" &&( passportFile==null ||passportFile==undefined))){
    swal("File Required","Please Select Passport","error")
    return false;
}
if(data.passportNo !="" && (data.passportExpireDate==null|| data.passportExpireDate=="")){
  swal("File Required","Please Enter  Passport Expire Date","error")
  return false;
}

if((( passportFile!=null && passportFile!=undefined)|| data.passportFile!="" )&& data.passportNo ==""){
  swal("File Required","Please Enter Passport Number","error")
  return false;
}

  
    var formData= new FormData();
    
    formData.append('Id',userData.id)
    formData.append('Name',data.name)
    formData.append('MobileNo',data.mobileNo)
    formData.append('Email',data.email)
    formData.append('EmployeeType',data.employeeType)
    formData.append('LocationId',data.locationId)
    formData.append('Designation',data.designation)
    formData.append('Nationality',data.nationality)
    formData.append('PassportNo',data.passportNo)
    formData.append('PassportExpireDate',data.passportExpireDate)
    formData.append('PassportFile',passportFile)
    formData.append('EmirateIdFile',emirateIdFile)
    formData.append('DrivingLicenceFile',drivingLicenceFile)
    formData.append('PersonPhoto',personPhoto)

    updateData(formData,handleClose,setError,clearErrors,refreshUserTable);

    //handleClose();
  };

  const handlefileChange = (e,t) => {
    switch(t){
        case 'passport': setpassportFile(e.target.files[0])
        break;
        case 'emirateId': setemirateIdFile(e.target.files[0])
        break;
        case 'drivingLicence': setdrivingLicenceFile(e.target.files[0])
        break;
        case 'personPhoto': setpersonPhoto(e.target.files[0])
        break;
    }
  };
  const refreshUserTable=()=>{
    refreshTable();
  }
  const handleClose = () => {

    setpassportFile(null);
    setemirateIdFile(null);
    setdrivingLicenceFile(null);
    setpersonPhoto(null);
    reset();
    setShowModal(false);
    

  };

  return (
    <>
     
      <Modal show={showModal} onHide={handleClose} size="lg">
      <Form autoComplete='off' onSubmit={handleSubmit(onSubmit)}>
        <Modal.Header closeButton>
          <Modal.Title>Add User</Modal.Title>
        </Modal.Header>

        <Modal.Body>
        <input type="hidden"
                {...register("id",)}
              
                defaultValue={userData.id}></input>
            <Row>
             <Col>
            <Form.Group controlId="controlname">
              <Form.Label>User Name</Form.Label>
              <input type="text" className='form-control' aria-invalid={errors.name ?true:false}
                {...register("name",{required:{value:true,message:"Please Enter User Name"},pattern:{...rules.UserName}})}
                placeholder="Enter User Name"
                defaultValue={userData.name}></input>
              
              {errors.name && (
                <p className='error'>{errors.name.message}</p>
              )}
            </Form.Group>
            </Col>
            <Col>
            <Form.Group controlId="mobilecontrol">
              <Form.Label>Mobile No</Form.Label>
              <input type='text' className='form-control' aria-invalid={errors.mobileNo ?true:false}
              {...register("mobileNo",{required:{value:true,message:"Please Enter Mobile Number"},pattern:{...rules.Mobile}})}
              placeholder="Enter Mobile No"
              defaultValue={userData.mobileNo}></input>
              {errors.mobileNo && (
                <p className='error'>{errors.mobileNo.message}</p>
              )}
            </Form.Group>
            </Col>
            </Row>
        
            <Row>
             <Col>
            <Form.Group controlId="controlemail">
              <Form.Label>Email Id</Form.Label>
              <input type='text' className='form-control' aria-invalid={errors.email ?true:false}
               {...register("email",{required:{value:true,message:"Please Enter Email Id"},pattern:{...rules.Email}})}
                placeholder="Enter Email Id" defaultValue={userData.email} />

              {errors.email && (
                <p className='error '>{errors.email.message}</p>
              )}
            </Form.Group>
            </Col>
            <Col>
            <Form.Group controlId="controlemployeeType">
              <Form.Label>Employee Type</Form.Label>
              <select className='form-select' aria-invalid={errors.employeeType ?true:false} {...register("employeeType",{required:{value:true,message:"Please Select Employee Type."}})}>
                <option value={""}>--- Select ---</option>
                {employeeTypeList.map(e => 
                    <option key={e} value={e}>{e}</option>
                    )}
            </select>
            {errors.employeeType && (
                <p className='error '>{errors.employeeType.message}</p>
              )}
            </Form.Group>
            </Col>
            </Row>
            <Row>
            <Col>
            <Form.Group controlId="controldesignation">
              <Form.Label>Designation</Form.Label>
              <select className='form-select' aria-invalid={errors.designation ?true:false} {...register("designation",{required:{value:true,message:"Please Select Employee Designation."}})}>
              <option value={""}>--- Select ---</option>
                {Designatios.map(d => 
                    <option key={d} value={d}>{d}</option>
                    )}
            </select>
                  {errors.designation && (
                        <p className='error '>{errors.designation.message}</p>
                 )}

            </Form.Group>
            </Col>
            <Col>
            <Form.Group controlId="controllocationId">
              <Form.Label>Location</Form.Label>
              <select className='form-select' aria-invalid={errors.locationId ?true:false} {...register("locationId",{required:{value:true,message:"Please Select Location."}})}>
                     <option value={""}>--- Select ---</option>
                        {locations && locations.length > 0 && locations.map(location =>
                     <option key={location.value} value={location.value}>{location.text}</option>
                     )}
               </select>
                    { errors.locationId &&
                    (
                        <p className='error '>{errors.locationId.message}</p>
                    )}
            </Form.Group>
            </Col>
            </Row>

            <Row>
            <Col>
            <Form.Group controlId="controlnationality">
              <Form.Label>Nationality</Form.Label>
              <select className='form-select' aria-invalid={errors.nationality ?true:false} {...register("nationality",{required:{value:true,message:"Please Select Nationality."}})}>
              <option value={""}>--- Select ---</option>
                {nationalityList.map(n =>  
                    <option key={n} value={n}>{n}</option>
                    )}
               </select>
                    { errors.nationality &&
                    (
                        <p className='error '>{errors.nationality.message}</p>
                    )}
             
            </Form.Group>
            </Col>
            <Col>
            <Form.Group controlId="controlpassportNo">
              <Form.Label>Passport No</Form.Label>
              <input type='text' className='form-control' aria-invalid={errors.passportNo ?true:false}
               placeholder="Enter Passport No" defaultValue={userData?.passportNo==null ?"":userData?.passportNo} {...register("passportNo",{pattern:{...rules.Passport}})}></input>
               {errors.passportNo && (
                <p className='error ' >{errors.passportNo.message}</p>
              )}
            </Form.Group>
            </Col>
            </Row>
            <Row>
                <Col>
                <Form.Group controlId="controlpassportFile">
                 <Form.Label>Passport File</Form.Label>
                <Form.Control type="file" name='passportFile' onChange={(event)=>handlefileChange(event,'passport')} />
                <p className='AiFillFileText'>
                </p>
                </Form.Group>
                </Col>
                
                <Col>
                <Form.Group controlId="controlpassportExpireDate">
                    <Form.Label>Passport Expire Date</Form.Label>
                    <input type="Date" className='form-control' 
                    {...register("passportExpireDate")} defaultValue={userData.passportExpireDate}></input>
                    {errors.passportExpireDate && (
                <p className='error '>{errors.passportExpireDate.message}</p>
              )}
                </Form.Group>
                </Col> 
            </Row>
            
            <Row>
            <Col>
                <Form.Group controlId="controlemirateIdFile">
                 <Form.Label>Emirate Id File</Form.Label>
                <Form.Control type="file" name='emirateIdFile' onChange={(event)=>handlefileChange(event,'emirateId')} />
                </Form.Group>
                </Col>
                <Col>
                <Form.Group controlId="controldrivingLicenceFile">
                 <Form.Label>Driving Licence File</Form.Label>
                <Form.Control type="file" name='drivingLicenceFile' onChange={(event)=>handlefileChange(event,'drivingLicence')} />
                </Form.Group>
                </Col>
            </Row>
            <Row>
            <Col>
                <Form.Group controlId="controlpersonPhoto">
                 <Form.Label>Person Photo</Form.Label>
                <Form.Control type="file" name='personPhoto' onChange={(event)=>handlefileChange(event,'personPhoto')} />
                </Form.Group>
                </Col>
            </Row>
            <Row className='my-3'>
              <Col className='px-3 col-12'>

              <div className='files w-100'>
          {
            userData?.passportFile &&(
              <div className='file-Icon'>
                <div>Passport</div>
                <BsFillFileEarmarkTextFill />
                <div>{userData?.passportFile?.split('/')[3].replaceAll('_',' _ ')}</div>
              </div>
            )
          }
          {
            userData?.emirateIdFile &&(
              <div className='file-Icon'>
                <div> Emirate Id </div>
                <BsFillFileEarmarkTextFill />
                <div>{userData?.emirateIdFile?.split('/')[3].replaceAll('_',' _ ')}</div>
              </div>
            )
          }
          {
            userData?.drivingLicenceFile &&(
              <div className='file-Icon'>
                <div> Driving Licence </div>
                <BsFillFileEarmarkTextFill style={{width:"64px"}}/>
                <div>{userData?.drivingLicenceFile?.split('/')[3].replaceAll('_',' _ ')}</div>
              </div>
            )
          }
          {
            userData?.personPhoto &&(
              <div className='file-Icon'>
                <div> Person Photo </div>
                <BsFillFileEarmarkTextFill />
                <div>{userData?.personPhoto?.split('/')[3].replaceAll('_',' _ ')}</div>
              </div>
            )
          }
              </div>

              </Col>
            </Row>
        </Modal.Body>

        <Modal.Footer>
        <Button variant="primary" type="submit">
              Submit
            </Button>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
        </Modal.Footer>
        </Form>
      </Modal>
      
    </>
  );
};

export default EditUserModal;
