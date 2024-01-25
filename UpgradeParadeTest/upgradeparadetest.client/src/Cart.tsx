
import { useNavigate } from 'react-router-dom';


function Cart({ products, quants }) {
    let total = 0;
    const navigate = useNavigate();
    const calculateSum = () => {
        let sum = 0;
        let result = []; 

        if (products != undefined) {
            products.forEach((pr) => {
                if (quants[pr.productName]>0) {
                    const quantity = quants[pr.productName] || 0;
                    const price = pr.productPrice * quantity;
                    total += price;
                    
                    const productData = {
                        productName: pr.productName,
                        quantity: quantity
                    };
                    result.push(productData);
                }
            });
        }

        return result; 
    };
    const arr = calculateSum();
    return (
        <div style={{ marginTop: 100, backgroundColor: 'red', padding: 50 }}>
            {calculateSum().map((product, index) => (
                <div key={index}>
                    <h4>{product.productName}</h4>
                    <p className="outerp">Count: {product.quantity}</p>
                </div>
            ))}
            <p className="outermp">Total: {total}</p>
            <button className="orderb" onClick={() => navigate('orderForm', { state: { total: total, result: arr } })}>Make order</button>
        </div>
    );

}

export default Cart;