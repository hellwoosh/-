using Keras.Models;
using Numpy;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace SerialKillerPredictionUI
{
    class Predictor
    {
        private string _modelPath;

        public Predictor(string modelPath)
        {
            _modelPath = modelPath;
        }

        public double Predict(double[] input)
        {
            double[] mean = File.ReadAllLines(_modelPath + "\\mean.csv").Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
            double[] std = File.ReadAllLines(_modelPath + "\\std.csv").Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
            var model = Model.LoadModel(_modelPath + "\\model");

            if (model is null) 
            {
                throw new System.Exception("Не удалось загрузить модель");
            }


            for (int i = 0; i < input.Length; i++)
            {
                input[i] -= mean[i];
                input[i] /= std[i];
            }

            var inputNP = np.array(input);
            var a = inputNP.reshape(new int[] { 1, 11 });
            var b = inputNP.reshape(new int[] { 11, 1 });
            var result = model.Predict(a);
            var rr = result.GetData<double>();

            return (float)rr[0];
        }
    }
}
