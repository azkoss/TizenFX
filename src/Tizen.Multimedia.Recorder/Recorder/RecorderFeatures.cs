/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Native = Interop.RecorderFeatures;

namespace Tizen.Multimedia
{
    /// <summary>
    /// The camera setting class provides methods/properties to get and
    /// set basic camera attributes.
    /// </summary>
    public class RecorderFeatures
    {
        internal readonly Recorder _recorder = null;

        private List<RecorderFileFormat> _fileFormats;
        private List<RecorderAudioCodec> _audioCodec;
        private List<RecorderVideoCodec> _videoCodec;
        private List<Size> _videoResolution;

        internal RecorderFeatures(Recorder recorder)
        {
            _recorder = recorder;
        }

        /// <summary>
        /// Retrieves all the file formats supported by the recorder.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="RecorderFileFormat"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera already has been disposed.</exception>
        public IEnumerable<RecorderFileFormat> SupportedFileFormats
        {
            get
            {
                if (_fileFormats == null)
                {
                    try
                    {
                        _fileFormats = new List<RecorderFileFormat>();

                        Native.FileFormatCallback callback = (RecorderFileFormat format, IntPtr userData) =>
                        {
                            _fileFormats.Add(format);
                            return true;
                        };
                        RecorderErrorFactory.ThrowIfError(Native.FileFormats(_recorder.GetHandle(), callback, IntPtr.Zero),
                        "Failed to get the supported fileformats");
                    }
                    catch
                    {
                        _fileFormats = null;
                        throw;
                    }
                }

                return _fileFormats;
            }
        }

        /// <summary>
        /// Retrieves all the audio encoders supported by the recorder.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="RecorderAudioCodec"/>.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera already has been disposed.</exception>
        public IEnumerable<RecorderAudioCodec> SupportedAudioEncodings
        {
            get
            {
                if (_audioCodec == null)
                {
                    try
                    {
                        _audioCodec = new List<RecorderAudioCodec>();

                        Native.AudioEncoderCallback callback = (RecorderAudioCodec codec, IntPtr userData) =>
                        {
                            _audioCodec.Add(codec);
                            return true;
                        };
                        RecorderErrorFactory.ThrowIfError(Native.AudioEncoders(_recorder.GetHandle(), callback, IntPtr.Zero),
                            "Failed to get the supported audio encoders");
                    }
                    catch
                    {
                        _audioCodec = null;
                        throw;
                    }
                }

                return _audioCodec;
            }
        }

        /// <summary>
        /// Retrieves all the video encoders supported by the recorder.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <returns>
        /// It returns a list containing all the supported <see cref="RecorderVideoCodec"/>.
        /// by recorder.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera already has been disposed.</exception>
        public IEnumerable<RecorderVideoCodec> SupportedVideoEncodings
        {
            get
            {
                if (_videoCodec == null)
                {
                    try
                    {
                        _videoCodec = new List<RecorderVideoCodec>();

                        Native.VideoEncoderCallback callback = (RecorderVideoCodec codec, IntPtr userData) =>
                        {
                            _videoCodec.Add(codec);
                            return true;
                        };
                        RecorderErrorFactory.ThrowIfError(Native.VideoEncoders(_recorder.GetHandle(), callback, IntPtr.Zero),
                            "Failed to get the supported video encoders");
                    }
                    catch
                    {
                        _videoCodec = null;
                        throw;
                    }
                }

                return _videoCodec;
            }
        }

        /// <summary>
        /// Retrieves all the video resolutions supported by the recorder.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <returns>
        /// It returns videoresolution list containing the width and height of
        /// different resolutions supported by recorder.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The camera already has been disposed.</exception>
        public IEnumerable<Size> SupportedVideoResolutions
        {
            get
            {
                if (_videoResolution == null)
                {
                    try
                    {
                        _videoResolution = new List<Size>();

                        Native.VideoResolutionCallback callback = (int width, int height, IntPtr userData) =>
                        {
                            _videoResolution.Add(new Size(width, height));
                            return true;
                        };
                        RecorderErrorFactory.ThrowIfError(Native.VideoResolution(_recorder.GetHandle(), callback, IntPtr.Zero),
                            "Failed to get the supported video resolutions.");
                    }
                    catch
                    {
                        _videoResolution = null;
                        throw;
                    }
                }

                return _videoResolution;
            }
        }
    }
}