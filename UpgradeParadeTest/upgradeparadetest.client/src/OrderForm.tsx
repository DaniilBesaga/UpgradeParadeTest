import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';

function OrderForm() {
    const location = useLocation();
    const [formData, setFormData] = useState({
        email: '',
        name: '',
        delivery: '',
        payment: '',
        prods: [],
        quantity: []
    });
    let prods = location.state.result.map((product, index) => {
  return {
    productName: product.productName,
    quantity: product.quantity
  };
});
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const orderInfo = {
            id: '00000000-0000-0000-0000-000000000000',
            deliveryMethod: formData.delivery,
            paymentMethod: formData.payment,
            totalPrice: location.state.total,
            products: [],
            customer: {
                id: '00000000-0000-0000-0000-000000000000',
                name: formData.name,
                email: formData.email     
            } 
        };
        prods.forEach((prod) => {
            for (let i = 0; i < prod.quantity; i++) {
                orderInfo.products.push({
                    id: '00000000-0000-0000-0000-000000000000',
                    name: prod.productName,
                    productPrice: 0.0,
                    productImg:''
                });
            }
        });
        console.log(orderInfo);
        fetch('order', {
            method: 'POST',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify(orderInfo)
        }).then(r => r.json()).then(res => {
            if (res) {
                alert("Success order!");
            }
        });
        if (formData.payment == "Bank card") {
            fetch('payments/create-checkout-session', {
                method: 'POST',
                headers: { 'Content-type': 'application/json' },
            }).then(r => r.json()).then(res => {
                if (res) {
                    alert("Success card payment!");
                }
            });
        }
    };
    let res = location.state.result;
    return (
        <form onSubmit={handleSubmit} action="post">
            <label htmlFor="email">Email<span style={{ color: 'red' }}>*</span></label>
            <input
                type="text"
                id="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
            />

            <label htmlFor="ns">Name<span style={{ color: 'red' }}></span></label>
            <input
                type="text"
                id="name"
                name="name"
                value={formData.name}
                onChange={handleChange}
            />

            <label htmlFor="delivery">Delivery Method<span style={{ color: 'red' }}>*</span></label>
            <select
                id="delivery"
                name="delivery"
                value={formData.delivery}
                onChange={handleChange}
            >
                <option disabled selected style={{ display: 'none' }}></option>
                <option>Nova Poshta</option>
                <option>Ukr Poshta</option>
            </select>
            {/* Add validation message here */}
            <label htmlFor="payment">Payment Method<span style={{ color: 'red' }}>*</span></label>
            <select
                id="payment"
                name="payment"
                value={formData.payment}
                onChange={handleChange}
            >
                <option disabled selected style={{ display: 'none' }}></option>
                <option>Cash on delivery</option>
                <option>Bank card</option>
            </select>
            {/* Add validation message here */}
            {res.map((product, index) => {
                formData.prods.push(product.productName);
                formData.quantity.push(product.quantity);
                return (
                    <div key={index}>
                        <h4>{product.productName}</h4>
                        <p>Count: {product.quantity}</p>
                    </div>
                );
            })}
            <p>For payment: {location.state.total}</p>

            <input type="submit" value="Continue"/>
        </form>
    );
}

export default OrderForm;