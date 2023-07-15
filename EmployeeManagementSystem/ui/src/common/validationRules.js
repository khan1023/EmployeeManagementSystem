var rules={
    Mobile:{value:/^(966)(5)[0-9]{8}$|^(971)[2345]{1}[0-9]{8}$|^(91)[6789]{1}[0-9]{9}/,
    message:"Please Enter Valid Mobile Number with Country Code."},
    Email:{value:/^[a-zA-Z0-9\.]{2,}@[a-z\-]+\.[a-z]{2,3}/,message:"Please Enter Valid Email Id"},
    UserName:{value:/^[a-zA-Z\s]{3,}/,message:"Please Enter Valid User Name"},
    Passport:{value:/^(?!^0+$)[a-zA-Z0-9]{3,20}$/,message:"Please Enter Valid Passport Number"}
}
export default rules;